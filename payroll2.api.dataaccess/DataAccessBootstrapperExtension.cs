using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Payroll2.Api.DataAccess.PayrollDbContext;
using Payroll2.Api.DataAccess.Repository;
using Payroll2.Api.DataAccess.UnitOfWork;
using Payroll2.Api.Framework.Dependency;

namespace Payroll2.Api.DataAccess
{
    public class DataAccessBootstrapperExtension : IBootstrapperExtension
    {
        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IPayrollContextFactory, PayrollContextFactory>();
        }
    }
}