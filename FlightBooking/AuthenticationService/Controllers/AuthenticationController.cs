using AuthenticationService.Services;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJWTManagerInterface iJWTManager;

        public AuthenticationController(IJWTManagerInterface jWTManager)
        {
            iJWTManager = jWTManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(AuthenticationUser user)
        {
            try
            {
                List<AuthenticationUser> users = iJWTManager.GetAllUsers().Where(a => a.Email.ToLower() == user.Email.ToLower()
                                                                                && a.Password == user.Password).ToList();
                if (users.Count() > 0)//User found in DB
                {
                    var token = iJWTManager.Authenticate(user);
                    if (token == null)
                    {
                        return Unauthorized();
                    }
                    return Ok(token);
                }
                else//User not ther in DB.
                {
                    return BadRequest("Invalid Email or Password");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
