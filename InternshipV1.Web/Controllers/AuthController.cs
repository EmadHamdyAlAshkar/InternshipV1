using InternshipV1.Service.AuthServices;
using InternshipV1.Service.AuthServices.Dtos;
using InternshipV1.Service.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipV1.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {
            var user = await _authService.Login(input);

            if (user == null)
                throw new Exception("Email Not Found");

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            var user = await _authService.Register(input);

            if (user == null)
                throw new Exception("Email Already Exist");

            return Ok(user);

        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateAdmin(RegisterDto input)
        {
            var user = await _authService.CreateAdmin(input);

            if (user == null)
                throw new Exception("Email Already Exist");

            return Ok(user);

        }
    }
}
