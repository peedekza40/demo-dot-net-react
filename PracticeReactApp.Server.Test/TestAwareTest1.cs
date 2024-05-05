using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using PracticeReactApp.Core.Constants;
using PracticeReactApp.Core.Data.Entities;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Server.Controllers;

namespace PracticeReactApp.Server.Test
{
    public class TestAwareTest1
    {
        [Theory]
        [InlineData(MenuCode.User, "[{\"Code\":\"US0001\",\"Name\":\"User\",\"Path\":\"/user\",\"Attribute\":null,\"Icon\":\"ic_user\",\"ParentCode\":null,\"Order\":1,\"IsDisplay\":true,\"IsActive\":true,\"RoleMenus\":[{\"Id\":1,\"RoleId\":\"ADMIN\",\"MenuCode\":\"US0001\",\"Role\":{\"Id\":\"ADMIN\",\"Name\":\"Admin\",\"NormalizedName\":\"ADMIN\",\"ConcurrencyStamp\":null}}]}]")]
        [InlineData(MenuCode.RoleManagement, "[{\"Code\":\"RM0001\",\"Name\":\"Role\",\"Path\":\"/role\",\"Attribute\":null,\"Icon\":\"ic_user\",\"ParentCode\":null,\"Order\":2,\"IsDisplay\":true,\"IsActive\":true,\"RoleMenus\":[{\"Id\":2,\"RoleId\":\"ADMIN\",\"MenuCode\":\"RM0001\",\"Role\":{\"Id\":\"ADMIN\",\"Name\":\"Admin\",\"NormalizedName\":\"ADMIN\",\"ConcurrencyStamp\":null}}]}]")]
        [InlineData("TEST00001", "null")]
        [InlineData(null, "null")]
        public void GetMenuByCode_Returns_Menu(string? menuCode, string? expectedResult)
        {
            //Act
            var result = controller.Test1(menuCode);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okResult?.Value?.ToString());
        }

        private readonly TestAwareController controller;

        public TestAwareTest1()
        {
            //Arrange
            var httpClient = new HttpClient();
            var menuRepositoryMock = new Mock<IMenuRepository>();
            var configurationMock = new Mock<IConfiguration>();

            //mock data
            var adminRole = new Role()
            {
                Id = RoleCode.Admin,
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            menuRepositoryMock
                .Setup(repo => repo.GetByCode(MenuCode.User))
                .Returns(new List<Menu>()
                {
                    new Menu()
                    {
                        Code = MenuCode.User,
                        Name = "User",
                        Path = "/user",
                        Icon = "ic_user",
                        Order = 1,
                        IsDisplay = true,
                        IsActive = true,
                        RoleMenus = new List<RoleMenu>()
                        {
                            new RoleMenu()
                            {
                                Id = 1,
                                RoleId = RoleCode.Admin,
                                MenuCode = MenuCode.User,
                                Role = adminRole
                            }
                        }
                    }
                });

            menuRepositoryMock
                .Setup(repo => repo.GetByCode(MenuCode.RoleManagement))
                .Returns(new List<Menu>()
                {
                    new Menu()
                    {
                        Code = MenuCode.RoleManagement,
                        Name = "Role",
                        Path = "/role",
                        Icon = "ic_user",
                        Order = 2,
                        IsDisplay = true,
                        IsActive = true,
                        RoleMenus = new List<RoleMenu>()
                        {
                            new RoleMenu()
                            {
                                Id = 2,
                                RoleId = RoleCode.Admin,
                                MenuCode = MenuCode.RoleManagement,
                                Role = adminRole
                            }
                        }
                    }
                });

            controller = new TestAwareController(httpClient, configurationMock.Object, menuRepositoryMock.Object);
        }
    }
}