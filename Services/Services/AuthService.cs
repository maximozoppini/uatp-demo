using AutoMapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using Services.Dtos;
using Services.Interfaces;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public AuthService(IUserRepository userRepository, IMapper mapper, IOptions<AppSettings> options) { 
            _userRepository = userRepository;
            _mapper = mapper;
            _appSettings = options.Value;
        }    
        public async Task<(bool status, string message)> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetByName(loginDto.UserName);

            if (user == null) {
                return (false, "User dont exist");
            }
            if (user.Password != loginDto.Password) {
                return (false, "Invalid password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_appSettings.JwtTokenSecret)),   
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            return (true, tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));
        }

        public async Task<(bool status, string message)> Register(UserRegistrationDto userDto)
        {
            var userExists = await _userRepository.GetByName(userDto.UserName);
            if (userExists != null) {
                return (false, "User already exists");
            }

            var user = await _userRepository.Add(_mapper.Map<User>(userDto));
            if (user == null) {
                return (false, "User creation failed");
            }

            return (true, "User creation successfully");
        }
    }
}
