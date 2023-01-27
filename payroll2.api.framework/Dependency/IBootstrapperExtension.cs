using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Payroll2.Api.Framework.Dependency
{
    public interface IBootstrapperExtension
    {
        void Initialize(IServiceCollection services, IConfiguration configuration);
    }
}