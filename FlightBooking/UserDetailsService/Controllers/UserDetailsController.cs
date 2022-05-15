using UserDetailsService.Models;
using UserDetailsService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using UserDetailsService.Common;
//using Authenticate;
//using Authenticate.Models;
//using Authenticate.Services;

namespace UserDetailsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        public readonly IUserInterface _userRepository;
        public UserDetailsController(IUserInterface userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// This method is for Registering new users.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        

        /// <summary>
        /// This method is for finding all the users from the DB.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    int loggedInUserType = 2;//Need to save this in session once user login.
                    if (loggedInUserType == (int)CommonEnums.UserType.Admin)
                    {
                        var users = _userRepository.GetAllUsers().ToList().Where(a => a.UserType == (int)CommonEnums.UserType.User);
                        scope.Complete();
                        return Ok(users);
                    }
                    else
                    {
                        return BadRequest("Only admin can able to see all users details");
                    }
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// This method is for finding the user details from userid.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns> 
        [Authorize]
        [HttpGet]
        [Route("GetUserDetails/{userId}")]
        public UserModel GetUserDetails(int userId)
        {
            try
            {
                return _userRepository.GetUserDetails(userId);      
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        //[Authorize]
        //[HttpGet]
        //[Route("GetUserDetailsFromEmail/{email}")]
        //public UserModel GetUserDetailsFromEmail(string email)
        //{
        //    try
        //    {
        //        List<UserModel> allUsers = _userRepository.GetAllUsers().ToList().Where(a => a.Email.ToLower() == email.ToLower()).ToList();
        //        if(allUsers.Count() > 0)
        //        {
        //            return allUsers[0];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// This method is for validating the user and generate token.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("Login")]
        //public IActionResult Login(User user)
        //{
        //    try
        //    {
        //        List<UserModel> users = _userRepository.GetAllUsers().ToList().Where(a => a.Email == user.Name && a.Password == user.Password).ToList();
        //        if (users.Count() >= 1)
        //        {
        //            Authenticate.Controllers.UserController userController;
        //            return userController.Authenticate(user);
        //        }
        //        return Ok("Invalid credentials");
        //    }
        //    catch(Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}
