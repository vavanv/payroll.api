using System;
using System.Security.Claims;

namespace Payroll2.Api.Framework.Security
{
    public interface ISecurityContextInitializer
    {
        void Initialize(ClaimsPrincipal principal);
        void InitializeUnauthenticated();
    }
}