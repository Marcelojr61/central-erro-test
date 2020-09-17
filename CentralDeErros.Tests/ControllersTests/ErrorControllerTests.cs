using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CentralDeErros.API;
using CentralDeErros.API.Controllers;
using CentralDeErros.Model.Models;
using CentralDeErros.Services;
using CentralDeErros.Services.Interfaces;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CentralDeErros.ControllersTests
{
    public class ErrorControllerTests
    {
        private readonly IMapper mapper;
        private readonly List<Error> errors;
        public ErrorControllerTests()
        {
            var configuration = new MapperConfiguration(x => x.AddProfile(new AutoMapperProfile()));
            mapper = new Mapper(configuration);
            errors = FakeContext.Errors;
        }

        [Fact]
        public void Create_ShouldReturnOkResult()
        {
            //Arrange
            var mockService = new Mock<IErrorService>();
            mockService.Setup(x => x.Register(It.IsAny<Error>()));

            var controller = new ErrorController(mockService.Object, mapper);

            //Act
            var contentResult = controller.Post(new ErrorEntryDTO
            {
                Details = "detail",
                Title = "title",
                Origin = "1.0.0.1",
                EnviromentId = 1,
                LevelId = 1,
                MicrosserviceClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031")
            });
            //Assert
            Assert.IsType<OkObjectResult>(contentResult.Result);
        }

        [Fact]
        public void Delete_ShouldReturnOkResult()
        {
            //Arrange
            var mockService = new Mock<IErrorService>();
            mockService.Setup(x => x.Delete(It.IsAny<Error>()));

            var controller = new ErrorController(mockService.Object, mapper);

            //Act
            var contentResult = controller.Delete(1);
            //Assert
            Assert.IsType<OkResult>(contentResult);
        }

        [Fact]
        public void Update_ShouldReturnOkResult()
        {
            //Arrange
            var dateTime = DateTime.Now;
            Error updatedError = new Error
            {
                Id = 1,
                Details = "detail",
                Title = "title",
                Origin = "1.0.0.1",
                ErrorDate = dateTime,
                EnviromentId = 1,
                LevelId = 1,
                MicrosserviceClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031")
            };


            var mockService = new Mock<IErrorService>();
            mockService.Setup(x => x.Update(It.IsAny<Error>()))
                .Returns(updatedError);
            mockService.Setup(x => x.CheckError(It.IsAny<int>()))
                .Returns((int id) => errors.Any(x => x.Id == id));


            var controller = new ErrorController(mockService.Object, mapper);

            //Act
            var contentResult = controller.Put(new ErrorEntryDTO
            {
                Id = 1,
                Details = "detail",
                Title = "title",
                Origin = "1.0.0.1",
                EnviromentId = 1,
                LevelId = 1,
                MicrosserviceClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031")
            });
            //Assert
            Assert.IsType<OkResult>(contentResult.Result);
        }

        [Fact]
        public void GetItems_ShouldReturnOkResult()
        {
            //Arrange
            var mockService = new Mock<IErrorService>();
            mockService.Setup(x => x.List(null, null, false))
                .Returns(errors);

            var expected = errors;

            var controller = new ErrorController(mockService.Object, mapper);

            //Act
            var contentResult = controller.List(null, null);
            //Assert
            Assert.IsType<OkObjectResult>(contentResult.Result);
            var actual = (contentResult.Result as OkObjectResult).Value as IEnumerable<ErrorDTO>;
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count());
        }

        [Fact]
        public void GetItemsById_ShouldReturnOkResult()
        {
            int requestId = 1;

            //Arrange
            var mockService = new Mock<IErrorService>();
            mockService.Setup(x => x.Fetch(It.IsAny<int>()))
                .Returns((int id) => errors.FirstOrDefault(x => x.Id == id));
            var expected = errors.FirstOrDefault(x => x.Id == requestId);

            var controller = new ErrorController(mockService.Object, mapper);

            //Act
            var contentResult = controller.Get(requestId);
            //Assert
            Assert.IsType<OkObjectResult>(contentResult.Result);
            var actual = (contentResult.Result as OkObjectResult).Value as ErrorDTO;
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
        }


    }
}
