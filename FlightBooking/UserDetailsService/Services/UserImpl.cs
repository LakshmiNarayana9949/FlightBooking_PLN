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
        public UserRegisterDbContext _UserRegisterDbContext;

        public UserImpl(UserRegisterDbContext userRegisterDbContext)
        {
            _UserRegisterDbContext = userRegisterDbContext;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _UserRegisterDbContext.UserRegistor.ToList();
        }

        public void AddNewUser(UserModel user)
        {
            _UserRegisterDbContext.UserRegistor.Add(user);
            _UserRegisterDbContext.SaveChanges();
        }

        public UserModel GetUserDetails(int userId)
        {
            return _UserRegisterDbContext.UserRegistor.ToList().Where(a => a.UserID == userId).ToList()[0];
        }
    }
}
