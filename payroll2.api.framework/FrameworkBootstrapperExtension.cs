using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Payroll2.Api.Framework.Dependency;
using Payroll2.Api.Framework.Security;

namespace Payroll2.Api.Framework
{
    public class FrameworkBootstrapperExtension : IBootstrapperExtension
    {
        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppSettingsService, AppSettingsService>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<ISecurityContext, SecurityContext>();
            services.AddSingleton<ISecurityContextAccessor, SecurityContextAccessor>();
            services.AddScoped<ISecurityContextInitializer, SecurityContextInitializer>();
        }
    }
}