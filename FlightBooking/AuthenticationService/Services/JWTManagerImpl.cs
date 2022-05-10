using AuthenticationService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.DBContext;

namespace AuthenticationService.Services
{
    public class JWTManagerImpl : IJWTManagerInterface
    {
        private readonly IConfiguration configuartion;
        private readonly AuthenticationServiceDbContext _authenticationServiceDbContext;

        public JWTManagerImpl(IConfiguration iconfiguration, AuthenticationServiceDbContext authenticationServiceDbContext)
        {
            configuartion = iconfiguration;
            _authenticationServiceDbContext = authenticationServiceDbContext;
        }

        
        public Tokens Authenticate(AuthenticationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuartion["JWT:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Email)
                }),
                Expires=DateTime.UtcNow.AddMinutes(10),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }

        public List<AuthenticationUser> GetAllUsers()
        {
            return _authenticationServiceDbContext.AuthenticationUsers.ToList();
        }
    }
}
