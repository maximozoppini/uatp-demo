using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool status, string message)> Register(UserRegistrationDto user);
        Task<(bool status, string message)> Login(LoginDto user);
    }
}
