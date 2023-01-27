using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Payroll2.Api.Framework.Dependency;
using Payroll2.Api.Mappers;

namespace Payroll2.Api
{
    public class MapperBootstrapperExtension : IBootstrapperExtension
    {
        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserMapper, UserMapper>();
            services.AddScoped<IMasterListMapper, MasterListMapper>();
            services.AddScoped<ICompanyMapper, CompanyMapper>();
            services.AddScoped<IEmployeeMapper, EmployeeMapper>();
        }
    }
}