using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Payroll2.Api.Framework.Dependency;
using Payroll2.Api.Services.Address;
using Payroll2.Api.Services.Company;
using Payroll2.Api.Services.Employee;
using Payroll2.Api.Services.MasterLists;
using Payroll2.Api.Services.Users;

namespace Payroll2.Api.Services
{
    public class ServicesBootstrapperExtension : IBootstrapperExtension
    {
        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IMasterListService, MasterListService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IAddressService, AddressService>();
        }
    }
}