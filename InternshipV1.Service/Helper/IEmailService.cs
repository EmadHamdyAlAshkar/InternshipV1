using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.Helper
{
    public interface IEmailService
    {
        Task SendEmail(string reciever, string subject, string body);
    }
}
