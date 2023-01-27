using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Payroll2.Api.Controllers;
using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Framework.Security;
using Payroll2.Api.Mappers;
using Payroll2.Api.Services.Users;

using Xunit;

namespace Payroll2.Api.Test
{
    public class UserControllerTests
    {
        [Fact]
        public async Task ControllerTest()
        {
            var securityContext = new Mock<ISecurityContext>();
            var mapper = new Mock<IUserMapper>();
            var userService = new Mock<IUserService>();

            var controller = new UserController(securityContext.Object, userService.Object, mapper.Object);
            var result = await controller.Get();
            var okRequestResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<SerializableError>(okRequestResult.Value);
        }

        [Fact]
        public void GetByUsernamePasswordTest()
        {
            var service = new Mock<IUserService>().Setup(s => s.GetUserByUsernamePassword("u", "p"))
                .Returns(Task.FromResult(new User()));
            Assert.NotNull(service);
        }

        [Fact]
        public void GetByUsernameTest()
        {
            var service = new Mock<IUserService>().Setup(s => s.GetUserById(1)).Returns(Task.FromResult(new User()));
            Assert.NotNull(service);
        }
    }
}