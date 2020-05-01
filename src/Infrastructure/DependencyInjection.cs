using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Files;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using CleanArchitecture.Infrastructure.HttpClients;
using CleanArchitecture.Infrastructure.Emails;
using CleanArchitecture.Infrastructure.Identity.ExternalIdentity;

namespace CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<SkipperAgencyDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<SkipperAgencyDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(SkipperAgencyDbContext).Assembly.FullName)));

            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<SkipperAgencyDbContext>());

            // Identity configuration
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<SkipperAgencyDbContext>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddUserManager<UserManager<AppUser>>()
                .AddDefaultTokenProviders();

            services.AddScoped<IJwtServicecs, JwtService>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddTransient<IHttpClient, BasicHttpClient>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<IExternalIdentityProviderFactory, ExternalIdentityProviderFactory>();
            services.AddScoped<FacebookIdentityProvider>()
                .AddScoped<IExternalIdentityProvider, FacebookIdentityProvider>(s => s.GetService<FacebookIdentityProvider>());
            services.AddScoped<GoogleIdentityProvider>()
                .AddScoped<IExternalIdentityProvider, GoogleIdentityProvider>(s => s.GetService<GoogleIdentityProvider>());

            services.AddTransient<ICloudStorageService, AzureStorageService>();
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
