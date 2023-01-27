using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Payroll2.Api.Framework.Security
{
    internal sealed class SecurityContextMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context, ISecurityContextInitializer contextInitializer,
            ISecurityContext securityContext, ISecurityContextAccessor securityContextAccessor)
        {
            securityContextAccessor.SecurityContext = securityContext;

            if (context.User?.Identity != null && context.User.Identity.IsAuthenticated)
                contextInitializer.Initialize(context.User);
            else
                contextInitializer.InitializeUnauthenticated();

            return _next(context);
        }
    }
}