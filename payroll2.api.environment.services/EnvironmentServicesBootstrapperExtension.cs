using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Payroll2.Api.Environment.Services.Systems;
using Payroll2.Api.Environment.Services.TenantProvider;
using Payroll2.Api.Framework.Dependency;

namespace Payroll2.Api.Environment.Services
{
    public class EnvironmentServicesBootstrapperExtension : IBootstrapperExtension
    {
        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDatabaseTenantProvider, DatabaseTenantProvider>();
            services.AddScoped<ISystemService, SystemService>();
        }
    }
}