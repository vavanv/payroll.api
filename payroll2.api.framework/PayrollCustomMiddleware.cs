using System;

using Microsoft.AspNetCore.Builder;

using Payroll2.Api.Framework.Exception;
using Payroll2.Api.Framework.Security;

namespace Payroll2.Api.Framework
{
    public static class PayrollCustomMiddleware
    {
        public static void UseCustomMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<SecurityContextMiddleware>();
            builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}