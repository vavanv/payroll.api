using System;
using System.Threading.Tasks;

using Moq;

using Payroll2.Api.Controllers;
using Payroll2.Api.Environment.Services.TenantProvider;
using Payroll2.Api.Framework.Security;
using Payroll2.Api.Services.Users;

using Xunit;

namespace Payroll2.Api.Test
{
    public class TokenControllerTest
    {
        [Fact]
        public async Task ControllerTest()
        {
            var userService = new Mock<IUserService>();
            var databaseTenantProvider = new Mock<IDatabaseTenantProvider>();
            var tokenProvider = new Mock<ITokenProvider>();

            var controller =
                new TokenController(userService.Object, tokenProvider.Object, databaseTenantProvider.Object);
            var tenant = databaseTenantProvider.Object.GetTenant();

            var x = await controller.GetToken();
        }
    }
}