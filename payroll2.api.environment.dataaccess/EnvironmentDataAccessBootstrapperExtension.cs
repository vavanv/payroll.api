using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Payroll2.Api.Environment.DataAccess.EnvironmentDbContext;
using Payroll2.Api.Environment.DataAccess.Repository;
using Payroll2.Api.Environment.DataAccess.UnitOfWork;
using Payroll2.Api.Framework.Dependency;

namespace Payroll2.Api.Environment.DataAccess
{
    public class EnvironmentDataAccessBootstrapperExtension : IBootstrapperExtension
    {
        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IEnvironmentContextFactory, EnvironmentContextFactory>();
        }
    }
}