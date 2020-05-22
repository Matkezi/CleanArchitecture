using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Infrastructure.Emails;
using SkipperAgency.Infrastructure.Files;
using SkipperAgency.Infrastructure.HttpClients;
using SkipperAgency.Infrastructure.Identity;
using SkipperAgency.Infrastructure.Identity.ExternalIdentity;
using SkipperAgency.Infrastructure.Identity.ExternalIdentity.Facebook;
using SkipperAgency.Infrastructure.Identity.ExternalIdentity.Google;
using SkipperAgency.Infrastructure.Persistence;
using SkipperAgency.Infrastructure.Services;
using System;
using System.Text;

namespace SkipperAgency.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<SkipperAgencyDbContext>(options =>
                    options.UseInMemoryDatabase("SkipperAgencyDb"));
            }
            else
            {
                services.AddDbContext<SkipperAgencyDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DbConnection"),
                        b => b.MigrationsAssembly(typeof(SkipperAgencyDbContext).Assembly.FullName)));

            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<SkipperAgencyDbContext>());

            // Identity configuration
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<SkipperAgencyDbContext>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddUserManager<UserManager<AppUser>>()
                .AddDefaultTokenProviders();

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"]));

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            }).AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = true,
                    ValidAudience = configuration["AppSettings:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = configuration["AppSettings:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddScoped<IJwtServicecs, JwtService>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IHttpClient, BasicHttpClient>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<IExternalIdentityProviderFactory, ExternalIdentityProviderFactory>();
            services.AddScoped<FacebookIdentityProvider>()
                .AddScoped<IExternalIdentityProvider, FacebookIdentityProvider>(s => s.GetService<FacebookIdentityProvider>());
            services.AddScoped<GoogleIdentityProvider>()
                .AddScoped<IExternalIdentityProvider, GoogleIdentityProvider>(s => s.GetService<GoogleIdentityProvider>());

            services.AddTransient<IFilesStorageService, FilesStorageService>();


            if (!int.TryParse(configuration["EmailSettings:Port"], out int port))
            {
                throw new ArgumentException("Email port from configuration exception.");
            }
            services.AddFluentEmail(configuration["EmailSettings:FromEmail"])
                .AddRazorRenderer()
                .AddSmtpSender(configuration["EmailSettings:Host"], port, configuration["EmailSettings:FromEmail"], configuration["EmailSettings:Password"]);

            return services;
        }
    }
}
