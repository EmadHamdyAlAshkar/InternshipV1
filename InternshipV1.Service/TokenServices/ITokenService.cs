using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.TokenServices
{
    public interface ITokenService
    {
        Task<string> GenerateTokenForLogin(IdentityUser identityuser);
    }
}
