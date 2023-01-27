using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

namespace Payroll2.Api.Framework.Security
{
    internal sealed class TokenProvider : ITokenProvider
    {
        private readonly IAppSettingsService _appSettingsService;

        public TokenProvider(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
        }

        public async Task<string> GetSignedToken(IEnumerable<Claim> claims)
        {
            var key = _appSettingsService.GetJwtSecurityKey();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                FrameworkConstants.PayrollIssuer,
                FrameworkConstants.PayrollAudience,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}