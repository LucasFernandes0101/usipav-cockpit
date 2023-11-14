using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using usipav_cockpit.Application.Interfaces;
using usipav_cockpit.Application.Services;
using usipav_cockpit.Domain.Interfaces;
using usipav_cockpit.Domain.Mapper;
using usipav_cockpit.Infrastructure.Contexts;
using usipav_cockpit.Infrastructure.Repositories;

namespace usipav_cockpit.Application
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddDependencies(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAutoMapperProfiles();
            service.AddDbContext(configuration);
            service.ResolveServices();
            service.ResolveRepositories();

            //Add JWT
            var keyJwt = configuration.GetSection("Keys").GetValue<string>("KEY_JWT") ?? string.Empty;

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(keyJwt)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            return service;
        }

        private static void AddAutoMapperProfiles(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(UserProfile));
            service.AddAutoMapper(typeof(SieveProfile));
        }

        private static void ResolveServices(this IServiceCollection services)
        {
            services.AddScoped<ISieveService, SieveService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        private static void ResolveRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISieveRepository, SieveRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            var usipavDbConnection = configuration.GetConnectionString("usipavdb");

            service.AddDbContextPool<SqlDbContext>(options =>
                options.UseMySql(usipavDbConnection, ServerVersion.AutoDetect(usipavDbConnection),
                b => b.MigrationsAssembly("usipav-cockpit")));
        }
    }
}
