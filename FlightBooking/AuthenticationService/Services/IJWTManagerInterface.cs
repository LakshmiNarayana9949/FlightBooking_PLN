using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    public interface IJWTManagerInterface
    {
        Tokens Authenticate(User users);
    }
}
