using IELTSExamPlatform.BL.Services.Abstractions;
using IELTSExamPlatform.BL.Services.Implements;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using AutoMapper;
using System.Reflection;
using IELTSExamPlatform.BL.Validators.Auth;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;

namespace IELTSExamPlatform.BL
{
    public static class ServiceRegistration
    {
        public static void AddBLServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IAuthService, AuthService>();


            //Validator
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(typeof(LoginDtoValidator));

            //Mapper

        }
    }
}
