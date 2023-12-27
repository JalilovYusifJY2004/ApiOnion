using ApiOnion104.Application.Abstractions.Services;
using ApiOnion104.Application.DTOs.Users;
using ApiOnion104.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiOnion104.Persistence.Implementations.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationService(IMapper mapper, IConfiguration configuration,UserManager<AppUser> userManager)
        {
            _mapper = mapper;
     _configuration = configuration;
            _userManager = userManager;
        }

        public async Task Regoster(RegisterDto dto)
        {

            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Name == dto.Name || u.Email == dto.Email);
            if (user is not null) throw new Exception("same");
            user = _mapper.Map<AppUser>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                StringBuilder message = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    message.AppendLine(error.Description);
                }
                throw new Exception(message.ToString());
            }

        }
        public async Task<string> Login(LoginDto dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.UserNameOrEmail);
            if (user is null)
            {
                 user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);
                if (user is null) throw new Exception("Username or email incorrect");

            }
            if (!await _userManager.CheckPasswordAsync(user, dto.Password)) throw new Exception("Username or email incorrect");
            ICollection<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Name)   ,
                new Claim(ClaimTypes.Surname,user.Surname)


            };
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
            SigningCredentials signing = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt: Issuer"],
                audience: _configuration["Jwt: Issuer"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signing
                );
            JwtSecurityTokenHandler handlerhandler = new JwtSecurityTokenHandler();
            return handlerhandler.WriteToken(token);

       


        }

    }
}
