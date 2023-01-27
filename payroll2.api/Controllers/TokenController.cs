using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Payroll2.Api.Environment.Services.TenantProvider;
using Payroll2.Api.Framework.Exception;
using Payroll2.Api.Framework.Security;
using Payroll2.Api.Services.Users;

namespace Payroll2.Api.Controllers
{
    public class TokenController : Controller
    {
        private const string BasicKeyWord = "Basic ";
        private readonly IDatabaseTenantProvider _databaseTenantProvider;
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserService _userService;

        public TokenController(IUserService userService, ITokenProvider tokenProvider,
            IDatabaseTenantProvider databaseTenantProvider)
        {
            _userService = userService;
            _tokenProvider = tokenProvider;
            _databaseTenantProvider = databaseTenantProvider;
        }

        [AllowAnonymous]
        [HttpPost("api/token")]
        public async Task<IActionResult> GetToken()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!HttpContext.Request.Headers.TryGetValue("Authorization", out var authorization))
                    {
                        throw new BusinessException("Header does not contain Authorization");
                    }

                    if (authorization.Count == 0 || !authorization[0].StartsWith(BasicKeyWord))
                    {
                        throw new BusinessException("Credentials are in wrong format");
                    }

                    var tenant = _databaseTenantProvider.GetTenant();
                    if (tenant == null)
                    {
                        throw new BusinessException("Tenant does not exist");
                    }

                    var encryptedCredentials = authorization[0].Substring(BasicKeyWord.Length).Trim();
                    var decryptedToken = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedCredentials));
                    var credentials = decryptedToken.Split(':');

                    if (string.IsNullOrWhiteSpace(credentials[0]) || string.IsNullOrWhiteSpace(credentials[1]))
                    {
                        throw new BusinessException("Credentials are in wrong format");
                    }

                    var username = Convert.ToString(credentials[0]);
                    var password = Convert.ToString(credentials[1]);
                    var user = await _userService.GetUserByUsernamePassword(username, password);

                    var claims = new[]
                    {
                        new Claim(PayrollClaimTypes.ClaimConstants.UserId, user.Id.ToString()),
                        new Claim(PayrollClaimTypes.ClaimConstants.TenantId, tenant.Id.ToString())
                    };

                    var token = await _tokenProvider.GetSignedToken(claims);

                    return Ok(token);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }
    }
}