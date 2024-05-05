using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Server.Controllers;
using System.Net;
using System.Net.Http.Json;

namespace PracticeReactApp.Server.Test
{
    public class TestAwareTest3
    {
        [Theory]
        [InlineData("1", PostId1Value)]
        [InlineData("2", PostId2Value)]
        public async Task GetPostById_Returns_Post(string id, string? expectedResult)
        {
            //Act
            var result = await controller.Test3(id);

            //Assert
            var expectedResultObject = new
            {
                url = $"{ApiUrl}{id}",
                method = "GET",
                response = JsonConvert.DeserializeObject<Dictionary<string, object>>(expectedResult)
            };

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(JsonConvert.SerializeObject(expectedResultObject), JsonConvert.SerializeObject(okResult?.Value));
        }

        [Fact]
        public async Task GetPostById_WhenInputIdIsNull_ShouldReturnErrorMessage()
        {
            //Act
            var result = await controller.Test3(null);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Id is required.", badRequestResult?.Value?.ToString());
        }

        [Theory]
        [InlineData("3")]
        [InlineData("4a")]
        [InlineData("6666")]
        [InlineData("999")]
        public async Task GetPostById_WhenInputNotExistsId_ShouldReturnErrorMessage(string id)
        {
            //Act
            var result = await controller.Test3(id);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult?.StatusCode);
            Assert.Equal("Failed to call the API.", objectResult?.Value?.ToString());
        }


        private const string PostId1Value = "{\"userId\": 1, \"id\": 1, \"title\": \"sunt aut facere repellat provident occaecati excepturi optio reprehenderit\", \"body\": \"quia et suscipit\\nsuscipit recusandae consequuntur expedita et cum\\nreprehenderit molestiae ut ut quas totam\\nnostrum rerum est autem sunt rem eveniet architecto\"}";

        private const string PostId2Value = "{\"userId\": 1, \"id\": 2, \"title\": \"qui est esse\", \"body\": \"est rerum tempore vitae\\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\\nqui aperiam non debitis possimus qui neque nisi nulla\"}";

        private const string ApiUrlKey = "Test3APIUrl";

        private const string ApiUrl = "https://jsonplaceholder.typicode.com//posts/";

        private readonly TestAwareController controller;

        public TestAwareTest3()
        {
            var postId1 = JsonContent.Create(JsonConvert.DeserializeObject<Dictionary<string, object>>(PostId1Value));

            var postId2 = JsonContent.Create(JsonConvert.DeserializeObject<Dictionary<string, object>>(PostId2Value));

            //Arrange
            var httpHandlerMock = new Mock<HttpMessageHandler>();

            var mockedPostId1Message = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = postId1
            };

            var mockedPostId2Message = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = postId2
            };

            var mockNotFoundMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            };

            var apiUrl1 = $"{ApiUrl}1";
            var apiUrl2 = $"{ApiUrl}2";

            httpHandlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                 ItExpr.Is<HttpRequestMessage>(rm => rm.RequestUri.AbsoluteUri.Equals(apiUrl1)),
                 ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(mockedPostId1Message);

            httpHandlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                 ItExpr.Is<HttpRequestMessage>(rm => rm.RequestUri.AbsoluteUri.Equals(apiUrl2)),
                 ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(mockedPostId2Message);

            httpHandlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                 ItExpr.Is<HttpRequestMessage>(rm => rm.RequestUri.AbsoluteUri != apiUrl1 && rm.RequestUri.AbsoluteUri != apiUrl2),
                 ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(mockNotFoundMessage);

            var inMemorySettings = new Dictionary<string, string> {
                {ApiUrlKey, ApiUrl}
            };

            var httpClient = new HttpClient(httpHandlerMock.Object);
            var menuRepositoryMock = new Mock<IMenuRepository>();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            controller = new TestAwareController(httpClient, configuration, menuRepositoryMock.Object);
        }
    }
}