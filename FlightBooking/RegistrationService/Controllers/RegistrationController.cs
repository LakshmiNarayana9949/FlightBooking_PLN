using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserDetailsService.Models;
using UserDetailsService.Services;

namespace RegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public readonly IUserInterface _userRepository;
        public RegisterController(IUserInterface userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        [Route("RegisterNewUser")]
        public IActionResult RegisterNewUser([FromBody] UserModel user)
        {
            try
            {
                _userRepository.AddNewUser(user);
                return Accepted();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
