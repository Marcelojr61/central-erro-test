using AutoMapper;
using CentralDeErros.API;
using CentralDeErros.API.Controllers;
using CentralDeErros.ControllerTest;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Interfaces;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;


namespace CentralDeErros.ControllersTests
{
    public class EnvironmentControllerTest : BaseControllerTest
    {
        Mock<IEnvironmentService> _serviceMock;
        EnvironmentController _controller;
        private readonly IMapper _mapper;

        public EnvironmentControllerTest()
        {
            var configuration = new MapperConfiguration(x => x.AddProfile(new AutoMapperProfile()));
            _mapper = new Mapper(configuration);

            _serviceMock = new Mock<IEnvironmentService>();
            _controller = new EnvironmentController(_serviceMock.Object, _mapper);
        }

        [Fact]
        public void GetAllEnvironments_ShouldCallService_AndReturn200WithDtos()
        {
            var expectedReturnFromService = new List<Environment>()
            {
                new Environment () { Id = 1, Phase = "Teste" },
                new Environment () { Id = 2, Phase = "T" }
            }.AsQueryable();

            _serviceMock.Setup(x => x.List()).Returns(expectedReturnFromService);

            var result = _controller.GetAllEnvironments();

            _serviceMock.Verify(x => x.List(), Times.Once);

            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);

            var dtos = Assert.IsType<List<EnvironmentDTO>>(objectResult.Value);
            Assert.NotEmpty(dtos);
            Assert.Equal(expectedReturnFromService.Count(), dtos.Count());

        }

        [Fact]
        public void GetEnviromentId_ShouldCallService_AndReturn200WithDtos_WhenLevelFound()
        {
            var expectedReturnFromService = new Environment() { Id = 1, Phase = "Teste" };

            _serviceMock.Setup(x => x.Fetch(1)).Returns(expectedReturnFromService);

            var result = _controller.GetEnviromentId(1);

            _serviceMock.Verify(x => x.Fetch(1), Times.Once);

            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);

            var dto = Assert.IsType<EnvironmentDTO>(objectResult.Value);
            Assert.Equal(expectedReturnFromService.Phase.ToLower(), dto.Phase);
        }

        [Fact]
        public void GetEnviromentId_ShouldCallService_AndReturn204_WhenLevelNoContent()
        {
            var result = _controller.GetEnviromentId(null);

            _serviceMock.Verify(x => x.Fetch(null), Times.Never);

            var objectResult = Assert.IsType<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
        }

        [Fact]
        public void SaveEnvironment_ShouldCallService_AndReturn200_WhenEverythingGoesRight()
        {
            var dto = new EnvironmentDTO { Phase = "Teste" };

            var level = new Environment { Id = 1, Phase = "Teste" };

            _serviceMock.Setup(x => x.RegisterOrUpdate(It.IsAny<Environment>())).Returns(level);

            var result = _controller.SaveEnvironment(dto);
            var validation = _controller.ModelState.IsValid;

            _serviceMock.Verify(x => x.RegisterOrUpdate(It.IsAny<Environment>()), Times.Once);

            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.True(validation);

        }

        [Fact]
        public void SaveEnvironment_ShouldCallService_AndReturn400WithError()
        {
            _controller.ModelState.AddModelError("test", "test");

            var result = _controller.SaveEnvironment(new EnvironmentDTO());
            var objectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void Validation_ModelState_False()
        {
            var dto = new EnvironmentDTO();

            var context = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isModelStateValid);
        }

        [Fact]
        public void Validation_ModelState_True()
        {
            var dto = new EnvironmentDTO { Id = 1, Phase = "Teste" };

            var context = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.True(isModelStateValid);
        }


        [Fact]
        public void UpdateEnvironment_ShouldCallService_AndReturn200WithDtos_WhenEnvironmentFound()
        {
            var environmentDTO = new EnvironmentDTO() { Id = 1, Phase = "Teste" };
            var expectedReturnFromService = new Environment() { Id = 1, Phase = "Teste" };

            _serviceMock.Setup(x => x.RegisterOrUpdate(It.IsAny<Environment>())).Returns(expectedReturnFromService);

            var result = _controller.UpdateEnvironment(environmentDTO);

            _serviceMock.Verify(x => x.RegisterOrUpdate(It.IsAny<Environment>()), Times.Once);

            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);

            var dto = Assert.IsType<EnvironmentDTO>(objectResult.Value);
            Assert.Equal(expectedReturnFromService.Phase.ToLower(), dto.Phase);
        }

        [Fact]
        public void UpdateEnvironment_ShouldCallService_AndReturn400WithError()
        {
            _controller.ModelState.AddModelError("test", "test");

            var result = _controller.UpdateEnvironment(new EnvironmentDTO());
            var objectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void DeleteEnvironmentId_ShouldCallService_AndReturn200()
        {

            _serviceMock.Setup(x => x.Delete(1));

            var result = _controller.DeleteEnvironmentId(1);

            _serviceMock.Verify(x => x.Delete(1), Times.Once);

            var objectResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void DeleteEnvironmentId_ShouldCallService_AndReturn204()
        {
            var result = _controller.DeleteEnvironmentId(null);

            _serviceMock.Verify(x => x.Delete(null), Times.Never);

            var objectresult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, objectresult.StatusCode);
        }
    }
}
