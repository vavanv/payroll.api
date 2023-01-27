using System;

namespace Payroll2.Api.Environment.Services.TenantProvider
{
    public interface IDatabaseTenantProvider
    {
        DataAccess.EnvironmentEntities.Systems GetTenant();
    }
}