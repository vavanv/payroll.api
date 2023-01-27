using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Payroll2.Api.Framework.Security;
using Payroll2.Api.Mappers;
using Payroll2.Api.Services.Users;

namespace Payroll2.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IUserMapper _mapper;
        private readonly ISecurityContext _securityContext;
        private readonly IUserService _userService;

        public UserController(ISecurityContext securityContext, IUserService userService, IUserMapper mapper)
        {
            _securityContext = securityContext;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("api/user")]
        public async Task<IActionResult> Get()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.GetUserById(_securityContext.UserId);
                    return Ok(_mapper.MapFrom(user));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        [HttpGet("api/fixpassword")]
        public async Task<IActionResult> Fix()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.FixPassword();
                    return Ok();
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