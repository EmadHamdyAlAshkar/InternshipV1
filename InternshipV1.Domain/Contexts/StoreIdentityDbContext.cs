using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Domain.Contexts
{
    public class StoreIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {
        }
    }
}
