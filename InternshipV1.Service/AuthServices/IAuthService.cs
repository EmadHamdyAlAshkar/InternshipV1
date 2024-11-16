using InternshipV1.Service.AuthServices.Dtos;
using InternshipV1.Service.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.AuthServices
{
    public interface IAuthService
    {
        Task<UserDto> Login(LoginDto input);
        Task<UserDto> Register(RegisterDto input);
        Task<UserDto> CreateAdmin(RegisterDto input);
    }
}
