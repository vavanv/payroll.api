using System;

namespace Payroll2.Api.Framework.Security
{
    public interface ISecurityContextAccessor
    {
        ISecurityContext SecurityContext { get; set; }
    }
}