using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CentralDeErros.API;
using CentralDeErros.API.Controllers;
using CentralDeErros.ControllerTest;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Interfaces;
using CentralDeErros.Transport.MicrosserviceDTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace CentralDeErros.ControllersTests
{
    public class MicrosserviceControllerTests : BaseControllerTest
    {

        readonly Mock<IMicrosserviceService> _serviceMock;
        readonly MicrosserviceController _controller;
        private readonly IMapper _mapper;
        private readonly ITokenGeneratorService _token;

        public MicrosserviceControllerTests()
        {
            var configuration = new MapperConfiguration(x => x.AddProfile(new AutoMapperProfile()));
            _mapper = new Mapper(configuration);

            _serviceMock = new Mock<IMicrosserviceService>();
            _controller = new MicrosserviceController(_serviceMock.Object, _mapper, _token);
        }

        [Fact]
        public void GetAllMicrosservices_ShouldCallService_AndReturn200WithDtos()
        {
            //Arrange
            var expectedReturnFromService = new List<Microsservice>()
            {
                new Microsservice () { ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"), Name = "Microsservice1", ClientSecret = "c4ca4238a0b923820dcc509a6f75849b" },
                new Microsservice () { ClientId = new Guid("3691b3e5-c411-4fd2-93d4-11a081552fb3"), Name = "Microsservice2", ClientSecret = "c81e728d9d4c2f636f067f89cc14862c" },
            }
            .AsQueryable();

            _serviceMock.Setup(x => x.List()).Returns(expectedReturnFromService);

            //Act
            var result = _controller.GetAllMicrosservices();

            _serviceMock.Verify(x => x.List(), Times.Once);

            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var dtos = Assert.IsType<List<MicrosserviceDTO>>(objectResult.Value);

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
            Assert.NotEmpty(dtos);
            Assert.Equal(expectedReturnFromService.Count(), dtos.Count());
        }

        [Fact]
        public void GetMicrosserviceById_ShouldCallService_AndReturn200WithDtos_WhenMicrosserviceFound()
        {
            //Arrange
            var expectedReturnFromService = new Microsservice() 
            { 
                ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"), 
                Name = "Microsservice1",
                ClientSecret = "c4ca4238a0b923820dcc509a6f75849b" 
            };
            var microsserviceClientOnlyDTO = new MicrosserviceClientIdOnlyDTO() { ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031") };

            //Act
            _serviceMock.Setup(x => x.Fetch(microsserviceClientOnlyDTO.ClientId)).Returns(expectedReturnFromService);

            var validation = _controller.ModelState.IsValid;

            var result = _controller.GetMicrosserviceById(microsserviceClientOnlyDTO.ClientId);
                     
            _serviceMock.Verify(x => x.Fetch(microsserviceClientOnlyDTO.ClientId), Times.Once);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
            var dto = Assert.IsType<MicrosserviceDTO>(objectResult.Value);
            Assert.Equal(expectedReturnFromService.ClientId, dto.ClientId);
            Assert.Equal(expectedReturnFromService.Name.ToLower(), dto.Name);
            Assert.True(validation);
        }

        [Fact]
        public void GetMicrosserviceById_ShouldCallService_AndReturn204_WhenMicrosserviceNotFound()
        {
            //Arrange
            var microsserviceClientOnlyDTO = new MicrosserviceClientIdOnlyDTO() { ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031") };

            //Act
            _serviceMock.Setup(x => x.Fetch(microsserviceClientOnlyDTO.ClientId));

            var validation = _controller.ModelState.IsValid;

            var result = _controller.GetMicrosserviceById(microsserviceClientOnlyDTO.ClientId);

            _serviceMock.Verify(x => x.Fetch(microsserviceClientOnlyDTO.ClientId), Times.Once);

            //Assert
            var objectResult = Assert.IsType<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
            Assert.True(validation);
        }

        [Fact]
        public void GetMicrosserviceById_ShouldCallService_AndReturn400WithError()
        {
            //Arrange
            _controller.ModelState.AddModelError("test", "test");

            //Act
            var result = _controller.GetMicrosserviceById(null);

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void DeleteMicrosserviceById_ShouldCallService_AndReturn200WithDtos_WhenMicrosserviceFound()
        {
            //Arrange
            var expectedReturnFromService = new Microsservice()
            {
                ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"),
                Name = "Microsservice1",
                ClientSecret = "c4ca4238a0b923820dcc509a6f75849b"
            };
            var microsserviceClientOnlyDTO = new MicrosserviceClientIdOnlyDTO() { ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031") };

            //Act
            _serviceMock.Setup(x => x.Fetch(microsserviceClientOnlyDTO.ClientId)).Returns(expectedReturnFromService);
            _serviceMock.Setup(x => x.Delete(microsserviceClientOnlyDTO.ClientId));

            var validation = _controller.ModelState.IsValid;

            var result = _controller.DeleteMicrosserviceById(microsserviceClientOnlyDTO.ClientId);

            _serviceMock.Verify(x => x.Fetch(microsserviceClientOnlyDTO.ClientId), Times.Once);
            _serviceMock.Verify(x => x.Delete(microsserviceClientOnlyDTO.ClientId), Times.Once);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.True(validation);
        }

        [Fact]
        public void DeleteMicrosserviceById_ShouldCallService_AndReturn204_WhenMicrosserviceNotFound()
        {
            //Arrange
            var microsserviceClientOnlyDTO = new MicrosserviceClientIdOnlyDTO() { ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031") };

            _serviceMock.Setup(x => x.Fetch(microsserviceClientOnlyDTO.ClientId));

            var validation = _controller.ModelState.IsValid;

            var result = _controller.DeleteMicrosserviceById(microsserviceClientOnlyDTO.ClientId);

            _serviceMock.Verify(x => x.Fetch(microsserviceClientOnlyDTO.ClientId), Times.Once);
            _serviceMock.Verify(x => x.Delete(new Microsservice()), Times.Never);

            //Assert
            var objectResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, objectResult.StatusCode);
            Assert.True(validation);
        }

        [Fact]
        public void DeleteMicrosserviceById_ShouldCallService_AndReturn400WithError()
        {
            //Arrange
            _controller.ModelState.AddModelError("test", "test");

            //Act
            var result = _controller.DeleteMicrosserviceById(null);

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void SaveMicrosservice_ShouldCallService_AndReturn200_WhenEverythingGoesRight()
        {
            var dto = new MicrosserviceNameOnlyDTO { Name = "Microsservice1" };

            var expectedReturnFromService = new Microsservice()
            {
                ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"),
                Name = "Microsservice1",
                ClientSecret = "c4ca4238a0b923820dcc509a6f75849b"
            };

            _serviceMock.Setup(x => x.Register(dto.Name)).Returns(expectedReturnFromService);

            var result = _controller.SaveMicrosservice(dto);
            var validation = _controller.ModelState.IsValid;

            _serviceMock.Verify(x => x.Register(dto.Name), Times.Once);

            var objectResult = Assert.IsType<CreatedResult>(result.Result);
            Assert.Equal(201, objectResult.StatusCode);
            Assert.True(validation);

        }

        [Fact]
        public void SaveMicrosservice_ShouldCallService_AndReturn400WithError()
        {
            _controller.ModelState.AddModelError("test", "test");

            var result = _controller.SaveMicrosservice(new MicrosserviceNameOnlyDTO());
            var objectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void Validation_ModelState_False()
        {
            var dto = new MicrosserviceNameOnlyDTO();

            var context = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isModelStateValid);
        }

        [Fact]
        public void Validation_ModelState_True()
        {
            var dto = new MicrosserviceNameOnlyDTO { Name = "Teste" };

            var context = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.True(isModelStateValid);
        }

        [Fact]
        public void UpdateMicrosserviceName_ShouldCallService_AndReturn200WithDtos_WhenMicrosserviceFound()
        {
            //Arrange
            var expectedReturnFromService = new Microsservice()
            {
                ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"),
                Name = "Microsservice1",
                ClientSecret = "c4ca4238a0b923820dcc509a6f75849b"
            };
            var microsserviceDTO = new MicrosserviceDTO()
            {
                ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"),
                Name = "Microsservice1"
            };

            //Act
            _serviceMock.Setup(x => x.Fetch(microsserviceDTO.ClientId)).Returns(expectedReturnFromService);
            _serviceMock.Setup(x => x.Update(It.IsAny<Microsservice>())).Returns(expectedReturnFromService);

            var validation = _controller.ModelState.IsValid;

            var result = _controller.UpdateMicrosserviceName(microsserviceDTO);

            _serviceMock.Verify(x => x.Fetch(microsserviceDTO.ClientId), Times.Once);
            _serviceMock.Verify(X => X.Update(It.IsAny<Microsservice>()), Times.Once);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
            var dto = Assert.IsType<MicrosserviceDTO>(objectResult.Value);
            Assert.Equal(expectedReturnFromService.ClientId, dto.ClientId);
            Assert.Equal(expectedReturnFromService.Name.ToLower(), dto.Name);
            Assert.True(validation);
        }

        [Fact]
        public void UpdateMicrosserviceName_ShouldCallService_AndReturn204_WhenMicrosserviceNotFound()
        {
            //Arrange
            var microsserviceDTO = new MicrosserviceDTO()
            {
                ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"),
                Name = "Microsservice1"
            };

            //Act
            _serviceMock.Setup(x => x.Fetch(microsserviceDTO.ClientId));

            var validation = _controller.ModelState.IsValid;

            var result = _controller.UpdateMicrosserviceName(microsserviceDTO);

            _serviceMock.Verify(x => x.Fetch(microsserviceDTO.ClientId), Times.Once);

            //Assert
            var objectResult = Assert.IsType<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
            Assert.True(validation);
        }

        [Fact]
        public void UpdateMicrosserviceName_ShouldCallService_AndReturn400WithError()
        {
            //Arrange
            _controller.ModelState.AddModelError("test", "test");

            //Act
            var result = _controller.UpdateMicrosserviceName(new MicrosserviceDTO());

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void RegenerateClientSecret_ShouldCallService_AndReturn200WithDtos_WhenMicrosserviceFound()
        {
            //Arrange
            var expectedReturnFromService = new Microsservice()
            {
                ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"),
                Name = "Microsservice1",
                ClientSecret = "c4ca4238a0b923820dcc509a6f75849b"
            };
            var microsserviceClientOnlyDTO = new MicrosserviceClientIdOnlyDTO() { ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031") };

            //Act
            _serviceMock.Setup(x => x.Fetch(microsserviceClientOnlyDTO.ClientId)).Returns(expectedReturnFromService);
            _serviceMock.Setup(x => x.GenerateClientSecret(It.IsAny<Microsservice>())).Returns(expectedReturnFromService);

            var validation = _controller.ModelState.IsValid;

            var result = _controller.RegenerateClientSecret(microsserviceClientOnlyDTO);

            _serviceMock.Verify(x => x.Fetch(microsserviceClientOnlyDTO.ClientId), Times.Once);
            _serviceMock.Verify(x => x.GenerateClientSecret(It.IsAny<Microsservice>()), Times.Once);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
            var dto = Assert.IsType<MicrosserviceRegisterDTO>(objectResult.Value);
            Assert.Equal(expectedReturnFromService.ClientId, dto.ClientId);
            Assert.Equal(expectedReturnFromService.ClientSecret, dto.ClientSecret);
            Assert.Equal(expectedReturnFromService.Name, dto.Name);
            Assert.True(validation);
        }

        [Fact]
        public void RegenerateClientSecret_ShouldCallService_AndReturn204_WhenMicrosserviceNotFound()
        {
            //Arrange
            var microsserviceClientOnlyDTO = new MicrosserviceClientIdOnlyDTO() { ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031") };

            //Act
            _serviceMock.Setup(x => x.Fetch(microsserviceClientOnlyDTO.ClientId));

            var validation = _controller.ModelState.IsValid;

            var result = _controller.RegenerateClientSecret(microsserviceClientOnlyDTO);

            _serviceMock.Verify(x => x.Fetch(microsserviceClientOnlyDTO.ClientId), Times.Once);

            //Assert
            var objectResult = Assert.IsType<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
            Assert.True(validation);
        }

        [Fact]
        public void RegenerateClientSecret_ShouldCallService_AndReturn400WithError()
        {
            //Arrange
            _controller.ModelState.AddModelError("test", "test");

            //Act
            var result = _controller.RegenerateClientSecret(new MicrosserviceClientIdOnlyDTO());

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }


    }
}
