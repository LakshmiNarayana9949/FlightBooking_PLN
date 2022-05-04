using UserDetailsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserDetailsService.Services
{
    public interface IUserInterface
    {
        IEnumerable<UserModel> GetAllUsers();
        void AddNewUser(UserModel user);
        UserModel GetUserDetails(int userId);
    }
}
