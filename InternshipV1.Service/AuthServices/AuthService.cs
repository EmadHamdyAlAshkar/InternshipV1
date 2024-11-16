using InternshipV1.Service.AuthServices.Dtos;
using InternshipV1.Service.TokenServices;
using InternshipV1.Service.UserServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(SignInManager<IdentityUser> signInManager,
                           UserManager<IdentityUser> userManager,
                           ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<UserDto> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);

            if (!result.Succeeded)
                throw new Exception("Login Failed");

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                UserName = user.UserName,
                Email = input.Email,
                Token = await _tokenService.GenerateTokenForLogin(user)
            };
        }

        public async Task<UserDto> Register(RegisterDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is not null)
                return null;

            var identityUser = new IdentityUser
            {
                UserName = input.UserName,
                Email = input.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, input.Password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault());

            await _userManager.AddToRoleAsync(identityUser, "User");

            return new UserDto
            {
                Id = Guid.Parse(identityUser.Id),
                UserName = identityUser.UserName,
                Email = identityUser.Email,
                Token = await _tokenService.GenerateTokenForLogin(identityUser)
            };
        }

        public async Task<UserDto> CreateAdmin(RegisterDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is not null)
                return null;

            var identityUser = new IdentityUser
            {
                UserName = input.UserName,
                Email = input.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, input.Password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault());

            await _userManager.AddToRoleAsync(identityUser, "Admin");

            return new UserDto
            {
                Id = Guid.Parse(identityUser.Id),
                UserName = identityUser.UserName,
                Email = identityUser.Email,
            };
        }
    }
}
