using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Server.Controllers;
using PracticeReactApp.Server.ViewModels.TestAware;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace PracticeReactApp.Server.Test
{
    public class TestAwareTest2
    {
        [Theory]
        [InlineData("1,2,1,3,5,4,2,4", "[{\"rank\":\"1\"},{\"rank\":\"2\"},{\"rank\":\"4\"}]")]
        [InlineData("A,B,1,2,1,AA,3,5,BB,4,2,4,AA,B", "[{\"rank\":\"AA\"},{\"rank\":\"B\"},{\"rank\":\"1\"},{\"rank\":\"2\"},{\"rank\":\"4\"}]")]
        [InlineData("i,3,E,6,3,i,c,1,w,E,N,u,M,u,M", "[{\"rank\":\"E\"},{\"rank\":\"M\"},{\"rank\":\"i\"},{\"rank\":\"u\"},{\"rank\":\"3\"}]")]
        [InlineData(null, "[]")]
        public void ArrangeCharItems_Returns_RankList(string? p1, string expectedResult)
        {
            //Act
            var result = controller.Test2(new Test2ViewModel() { P1 = p1 });

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, JsonConvert.SerializeObject(okResult?.Value));
        }

        [Theory]
        [InlineData("F,G,0,M,P,C,H,Q,5,5,Y,4,N,C,L,G,Q,2,H,5,V,N,A,P,U,P,K,Y,F,W,R,G,T,6,F,W,G,T,T,Q,7,4,B,6,S,5,4,0,Y,Z,2,6,N,Q,5,4,R,J,6,Z,7,X,9,5,H,J,7,T,N,H,5,K,D,C,J,K,R,L,D,Q,W,D,2,C,P,2,2,B,3,T,N,3,H,L,J,X,F,Z,5,0")]
        public void ArrangeCharItems_WhenInputLengthGreaterMaximum_ShouldReturnErrorMessage(string? p1)
        {
            //Act
            var model = new Test2ViewModel() { P1 = p1 };
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, validateAllProperties: true);

            //Assert
            Assert.False(isValid);
            Assert.Single(results);
            Assert.Equal($"The length of {nameof(Test2ViewModel.P1)} cannot exceed 99 items.", results[0].ErrorMessage);
        }

        private readonly TestAwareController controller;

        public TestAwareTest2()
        {
            //Arrange
            var httpClient = new HttpClient();
            var menuRepositoryMock = new Mock<IMenuRepository>();
            var configurationMock = new Mock<IConfiguration>();

            controller = new TestAwareController(httpClient, configurationMock.Object, menuRepositoryMock.Object);
        }
    }
}