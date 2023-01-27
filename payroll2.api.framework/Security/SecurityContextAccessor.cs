using System;
using System.Threading;

namespace Payroll2.Api.Framework.Security
{
    public sealed class SecurityContextAccessor : ISecurityContextAccessor
    {
        private static readonly AsyncLocal<ISecurityContext> SecurityContextLocal = new AsyncLocal<ISecurityContext>();

        public ISecurityContext SecurityContext
        {
            get => SecurityContextLocal.Value;
            set => SecurityContextLocal.Value = value;
        }
    }
}