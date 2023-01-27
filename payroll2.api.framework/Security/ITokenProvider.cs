using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Payroll2.Api.Framework.Security
{
    public interface ITokenProvider
    {
        Task<string> GetSignedToken(IEnumerable<Claim> claims);
    }
}