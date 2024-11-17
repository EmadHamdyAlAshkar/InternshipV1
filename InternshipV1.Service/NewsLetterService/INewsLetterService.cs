using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.NewsLetterService
{
    public interface INewsLetterService
    {
        public Task SendNewsletter( string subject, string body);
    }
}
