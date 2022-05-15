using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models
{
    public class AuthenticationUser
    {
        public int UserId { get; set; }
        public int UserType { get; set; }
        [Key]
    
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
