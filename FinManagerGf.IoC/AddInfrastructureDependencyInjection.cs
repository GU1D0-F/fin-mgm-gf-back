using FinManagerGf.Application.Repositories;
using FinManagerGf.Data;
using FinManagerGf.Domain.Entities;
using FinManagerGf.Infraestructure.Repositories;
using FinManagerGf.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinManagerGf.IoC
{
    public static class AddInfrastructureDependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, Dictionary<string, string> appSettingsValues)
        {
            //services
            //.AddScoped<CoreDbContext, PostgresDbContext>()
            //.AddDbContextFactory<CoreDbContext, PostgresDbContextFactory>(options =>
            //{
            //    options.UseNpgsql(appSettingsValues[AppSettingsPropertyNames.DefaultConnection]);
            //}, ServiceLifetime.Scoped);

            services.AddDbContext<PostgresDbContext>(opts =>
            {
               opts.UseNpgsql(appSettingsValues[AppSettingsPropertyNames.DefaultConnection]);
             });


            services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<PostgresDbContext>()
                .AddDefaultTokenProviders();

            InjectAuthConfiguration(services);
            InjectRepositories(services, appSettingsValues);
            return services;
        }


        private static void InjectAuthConfiguration(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.User.AllowedUserNameCharacters += " ";
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3A81F90D5A4E5E8A1C84E7D4B901D6BB67F4FD52415A84ECA7857D2B2E1A55F")),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = "fin-dev-identity",
                    ValidAudience = "fin-manager-front",
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.AddAuthorization();
        }

        private static void InjectRepositories(IServiceCollection services, Dictionary<string, string> appSettingsValues)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<IAppSettingsRepository>(new AppSettingsRepository(appSettingsValues));
        }
    }
}
