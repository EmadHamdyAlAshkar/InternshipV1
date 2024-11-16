using Microsoft.OpenApi.Models;

namespace InternshipV1.Web.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            //services.AddSwaggerGen();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "OMDA Store",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "API Support",
                            Email = "support@example.com",
                            Url = new Uri("https://example.com/support")
                        }
                    });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Please enter your token in the format **Bearer {token}**",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition("bearer", securityScheme);

                var sequrityRequirments = new OpenApiSecurityRequirement
                {
                    {securityScheme, new[] {"bearer" } }
                };

                options.AddSecurityRequirement(sequrityRequirments);
            });

            return services;
        }
    }
}
