using System;
using System.Security.Claims;

namespace Payroll2.Api.Framework.Security
{
    public static class PayrollClaimTypes
    {
        public static int GetTenantId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.GetClaimValue(ClaimConstants.TenantId, "0"));
        }

        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.GetClaimValue(ClaimConstants.UserId, "0"));
        }

        public static string GetClaimValue(this ClaimsPrincipal principal, string claimType, string defaultValue = null)
        {
            var claim = principal.FindFirst(claimType);
            if (claim == null) return defaultValue;

            return string.IsNullOrEmpty(claim.Value) || string.IsNullOrEmpty(claim.Value.Trim())
                ? null
                : claim.Value.Trim();
        }

        public class ClaimConstants
        {
            public const string TenantId = "payroll/claims/tenantId";
            public const string UserId = "payroll/claims/userId";
        }
    }
}