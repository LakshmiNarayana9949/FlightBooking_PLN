using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserDetailsService.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { set; get; }

        //[Required]
        //[StringLength(50, ErrorMessage = "Length Should no greater than 50 char")]
        public string FirstName { set; get; }
        //[Required]
        //[StringLength(50, ErrorMessage = "Length Should no greater than 50 char")]
        public string LastName { set; get; }
        //[Required]
        //[Phone]
        //[StringLength(20)]
        public string mobile { set; get; }
        //[Required]
        //[StringLength(200)]
        public string Address { set; get; }
        //[Required]
        //[EmailAddress]
        public string Email { set; get; }
        //[Required]
        //[DataType(DataType.Password)]
        public string Password { set; get; }
        //[Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { set; get; }

        public string CreatedBy { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Updatedby { set; get; }
        public DateTime UpdatedDate { set; get; }

    }
}
