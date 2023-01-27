using System;

namespace Payroll2.Api.Framework.Security
{
    internal sealed class SecurityContext : ISecurityContext
    {
        public int TenantId { get; set; }

        public int UserId { get; set; }
    }
}