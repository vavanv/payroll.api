using System;

using Microsoft.AspNetCore.Http;

using Payroll2.Api.Environment.Services.Systems;
using Payroll2.Api.Framework.Exception;
using Payroll2.Api.Framework.Security;

namespace Payroll2.Api.Environment.Services.TenantProvider
{
    public class DatabaseTenantProvider : IDatabaseTenantProvider
    {
        private readonly DataAccess.EnvironmentEntities.Systems _tenant;

        public DatabaseTenantProvider(ISystemService systemService, IHttpContextAccessor accessor,
            ISecurityContext securityContext)
        {
            if (securityContext.TenantId != 0)
            {
                _tenant = systemService.GetSystemByIdSync(securityContext.TenantId);
                return;
            }

            if (!accessor.HttpContext.Request.Headers.TryGetValue("Tenant", out var tenant))
                throw new BusinessException("Header does not contain Tenant");

            if (tenant.Count == 0) throw new BusinessException("Tenant is in a wrong format");

            _tenant = systemService.GetSystemByNameSync(tenant);
        }

        public DataAccess.EnvironmentEntities.Systems GetTenant()
        {
            return _tenant;
        }
    }
}