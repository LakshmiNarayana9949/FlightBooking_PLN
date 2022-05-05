using UserDetailsService.DBContext;
using UserDetailsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserDetailsService.Services
{
    public class UserImpl : IUserInterface
    {
        public UserDetailsDbContext _userDetailsDbContext;

        public UserImpl(UserDetailsDbContext userRegisterDbContext)
        {
            _userDetailsDbContext = userRegisterDbContext;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _userDetailsDbContext.Users.ToList();
        }

        public void AddNewUser(UserModel user)
        {
            _userDetailsDbContext.Users.Add(user);
            _userDetailsDbContext.SaveChanges();
        }

        public UserModel GetUserDetails(int userId)
        {
            return _userDetailsDbContext.Users.ToList().Where(a => a.UserID == userId).ToList()[0];
        }
    }
}
