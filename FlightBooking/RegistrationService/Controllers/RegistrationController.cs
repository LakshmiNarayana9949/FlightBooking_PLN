using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserDetailsService.Models;
using UserDetailsService.Services;
using System.Transactions;
using UserDetailsService.Common;

namespace RegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public readonly IUserInterface _iUserInterface;
        public RegisterController(IUserInterface iUserInterface)
        {
            _iUserInterface = iUserInterface;
        }
        [HttpPost]
        [Route("RegisterNewUser")]
        public IActionResult RegisterNewUser(UserModel user)
        {
            try
            {
                user.UserType = (int) CommonEnums.UserType.User;
                user.CreatedOn = DateTime.Now;
                bool emailAlreadyExists = IsEmailAlreadyExists(user.Email);
                if (!emailAlreadyExists)
                {
                    using (var scope = new TransactionScope())
                    {
                        _iUserInterface.AddNewUser(user);
                        scope.Complete();
                        return Ok(user.FirstName + " " + user.LastName + " " + "registered successfully.");
                    }
                }
                else
                {
                    return Ok("Email already exists");
                }
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        private bool IsEmailAlreadyExists(string email)
        {
            try
            {
                List<UserModel> usersList = _iUserInterface.GetAllUsers().ToList().Where(a => a.Email.ToLower() == email.ToLower()).ToList();
                if (usersList.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
