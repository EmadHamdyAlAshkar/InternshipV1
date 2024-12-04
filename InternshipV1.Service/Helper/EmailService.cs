using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.Helper
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(string reciever, string subject, string body)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("emadh732@gmail.com", "pwaxnuqgekfcphhl");

            client.Send("emadh732@gmail.com", reciever, subject, body);
        }
    }
}
