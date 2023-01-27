using System;

namespace Payroll2.Api.Framework.Security
{
    public interface ISecurityContext
    {
        int TenantId { get; }

        int UserId { get; }
    }
}