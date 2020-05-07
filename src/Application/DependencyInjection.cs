using AutoMapper;
using CleanArchitecture.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            // TODO: waiting for https://github.com/dotnet/runtime/pull/34393
            //services.AddTransient(typeof(IRequestPreProcessor<>), typeof(CharterAuthBehaviour<>));
            //services.AddTransient(typeof(IRequestPreProcessor<>), typeof(SkipperAuthBehaviour<>));
            //services.AddTransient(typeof(IRequestPreProcessor<>), typeof(CharterOrSkipperBookingsAuthBehaviour<>));
            //services.AddTransient(typeof(IRequestPreProcessor<>), typeof(CharterBookingsAuthBehaviour<>));
            //services.AddTransient(typeof(IRequestPreProcessor<>), typeof(SkipperBookingAuthBehaviour<>));
            //services.AddTransient(typeof(IRequestPreProcessor<>), typeof(CharterBoatAuthBehaviour<>));
            
            return services;
        }
    }
}
