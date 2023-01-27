using System;
using System.Security.Claims;

namespace Payroll2.Api.Framework.Security
{
    internal sealed class SecurityContextInitializer : ISecurityContextInitializer
    {
        private readonly SecurityContext _securityContext;

        public SecurityContextInitializer(ISecurityContext securityContext)
        {
            _securityContext = securityContext as SecurityContext;
        }

        public void Initialize(ClaimsPrincipal principal)
        {
            if (principal?.Identity == null) throw new ArgumentNullException(nameof(principal));

            if (!principal.Identity.IsAuthenticated) throw new InvalidOperationException("User is not authenticated.");

            _securityContext.TenantId = principal.GetTenantId();
            _securityContext.UserId = principal.GetUserId();
        }

        public void InitializeUnauthenticated()
        {
        }
    }
}