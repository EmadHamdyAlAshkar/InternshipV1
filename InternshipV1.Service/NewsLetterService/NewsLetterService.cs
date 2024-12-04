using InternshipV1.Service.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.NewsLetterService
{
    public class NewsLetterService : INewsLetterService
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;

        public NewsLetterService(IEmailService emailService,
                                 UserManager<IdentityUser> userManager)
        {
            _emailService = emailService;
            _userManager = userManager;
        }
        public async Task SendNewsletter( string subject, string body)
        {
            var emailAddresses = new List<string> { "emadh9074@hmail.com", "emadhamdyalashkar@gmail.com"};

            var usersEmails = _userManager.Users.Select(x => x.Email).ToList();

             Console.WriteLine(usersEmails);

            foreach (var mail in usersEmails)
            {
                await _emailService.SendEmail(mail, subject, body);
            }
        }
    }
}
