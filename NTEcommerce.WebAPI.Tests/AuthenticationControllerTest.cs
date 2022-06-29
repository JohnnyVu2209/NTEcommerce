using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using NTEcommerce.SharedDataModel;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Controllers;
using NTEcommerce.WebAPI.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.WebAPI.Tests
{
    public class AuthenticationControllerTest
    {
        
        [Fact]
        public async Task Test_Authentication_Login_Return_ResponseJWT()
        {
            var fakeUserManager = new Mock<FakeUserManager>();
            var config = new Mock<IConfiguration>();

            AuthenticationController controller = new(fakeUserManager.Object, config.Object);

            var result = await controller.Login(new LoginModel { Username = "Admin", Password = "Admin123" });

            Assert.NotNull(result);
        }

        private User GetUser()
        {
            return new User
            {
                UserName = RoleConstant.Admin,
                FullName = "ABC XYZ",
                NormalizedUserName = RoleConstant.Admin
            };
        }
    }
}
