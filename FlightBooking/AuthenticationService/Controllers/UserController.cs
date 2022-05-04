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
    public class UserController : ControllerBase
    {
        private readonly IJWTManagerInterface iJWTManager;

        public UserController(IJWTManagerInterface jWTManager)
        {
            iJWTManager = jWTManager;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public List<string> GetAllUsers()
        {
            var users = new List<string>();
            return users;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(User userdata)
        {
            var token = iJWTManager.Authenticate(userdata);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
