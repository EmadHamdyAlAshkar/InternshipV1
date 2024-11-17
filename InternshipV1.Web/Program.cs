
using InternshipV1.Domain.Contexts;
using InternshipV1.Repository.Interfaces;
using InternshipV1.Repository.Repositories;
using InternshipV1.Service.ProductServices;
using InternshipV1.Service.ProductServices.Dtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InternshipV1.Repository;
using InternshipV1.Service.AuthServices;
using InternshipV1.Service.TokenServices;
using InternshipV1.Web.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hangfire;
using InternshipV1.Service.Helper;
using InternshipV1.Service.NewsLetterService;

namespace InternshipV1.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


            builder.Services.AddHangfire(options =>
                       options
                       //.UseSimpleAssemblyNameTypeSerializer()
                       //.UseRecommendedSerializerSettings()
                       .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

            builder.Services.AddHangfireServer();


            builder.Services.AddScoped<IproductRepository, productRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<INewsLetterService, NewsLetterService>();
            builder.Services.AddAutoMapper(typeof(ProductProfile));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContext>()
            .AddDefaultTokenProviders();

            //builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    ValidateAudience = false
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDocumentation();

            var app = builder.Build();

            async Task ApplyMigrationsAsync(WebApplication app) { 
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
                await dbContext.Database.MigrateAsync();
            }
            }

            ApplyMigrationsAsync(app);

            async Task SeedIdentityData(WebApplication app)
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    await StoreIdentityContextSeed.SeedRolesAndSuperAdmin(services);
                }
            }

            SeedIdentityData(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();
            app.UseHangfireServer();


            app.MapControllers();

            app.Run();

            
        }
    }
}
