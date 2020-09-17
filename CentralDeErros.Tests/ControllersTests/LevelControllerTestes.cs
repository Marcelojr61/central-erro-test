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
    public class LevelControllerTestes : BaseControllerTest
    {
        Mock<ILevelService> _serviceMock;
        LevelController _controller;
        private readonly IMapper _mapper;

        public LevelControllerTestes()
        {
            var configuration = new MapperConfiguration(x => x.AddProfile(new AutoMapperProfile()));
            _mapper = new Mapper(configuration);

            _serviceMock = new Mock<ILevelService>();
            _controller = new LevelController(_serviceMock.Object, _mapper);
        }

        [Fact]
        public void GetAllLevel_ShouldCallService_AndReturn200WithDtos()
        {
            //Arrange
            var expectedReturnFromService = new List<Level>()
            {
                new Level () { Id = 1, Name = "Teste" },
                new Level () { Id = 2, Name = "T" }
            }.AsQueryable();

            _serviceMock.Setup(x => x.List()).Returns(expectedReturnFromService);

            //Act
            var result = _controller.GetAllLevel();

            _serviceMock.Verify(x => x.List(), Times.Once);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
            var dtos = Assert.IsType<List<LevelDTO>>(objectResult.Value);
            Assert.NotEmpty(dtos);
            Assert.Equal(expectedReturnFromService.Count(), dtos.Count());

        }

        [Fact]
        public void GetLevelId_ShouldCallService_AndReturn200WithDtos_WhenLevelFound()
        {
            var expectedReturnFromService = new Level() { Id = 1, Name = "Teste" };

            _serviceMock.Setup(x => x.Fetch(1)).Returns(expectedReturnFromService);

            var result = _controller.GetLevelById(1);

            _serviceMock.Verify(x => x.Fetch(1), Times.Once);

            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);

            var dto = Assert.IsType<LevelDTO>(objectResult.Value);
            Assert.Equal(expectedReturnFromService.Name.ToLower(), dto.Name);
        }

        [Fact]
        public void GetLevelId_ShouldCallService_AndReturn204_WhenLevelNoContent()
        {
            var result = _controller.GetLevelById(null);

            _serviceMock.Verify(x => x.Fetch(null), Times.Never);

            var objectResult = Assert.IsType<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
        }

        [Fact]
        public void SaveLevel_ShouldCallService_AndReturn200_WhenEverythingGoesRight()
        {
            var dto = new LevelDTO { Name = "Teste"};

            var level = new Level { Id = 1, Name = "Teste" };

            _serviceMock.Setup(x => x.RegisterOrUpdate(It.IsAny<Level>())).Returns(level);
            
            var result = _controller.SaveLevel(dto);
            var validation = _controller.ModelState.IsValid;

            _serviceMock.Verify(x => x.RegisterOrUpdate(It.IsAny<Level>()), Times.Once);

            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.True(validation);

        }

        [Fact]
        public void SaveLevel_ShouldCallService_AndReturn400WithError()
        {
            _controller.ModelState.AddModelError("test", "test");

            var result = _controller.SaveLevel(new LevelDTO());
            var objectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void Validation_ModelState_False()
        {
            var dto = new LevelDTO();

            var context = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isModelStateValid);
        }

        [Fact]
        public void Validation_ModelState_True()
        {
            var dto = new LevelDTO { Id = 1, Name = "Teste" };

            var context = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.True(isModelStateValid);
        }


        [Fact]
        public void UpdateLevel_ShouldCallService_AndReturn200WithDtos_WhenLevelFound()
        {
            var LevelDTO = new LevelDTO() { Id = 1, Name = "Teste" };
            var expectedLevelReturn = new Level() { Id = 1, Name = "Teste" };

            _serviceMock.Setup(x => x.RegisterOrUpdate(It.IsAny<Level>())).Returns(expectedLevelReturn);

            var result = _controller.UpdateLevel(LevelDTO);

            _serviceMock.Verify(x => x.RegisterOrUpdate(It.IsAny<Level>()), Times.Once);

            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);

            var dto = Assert.IsType<LevelDTO>(objectResult.Value);
            Assert.Equal(expectedLevelReturn.Name.ToLower(), dto.Name);
        }

        [Fact]
        public void UpdateLevel_ShouldCallService_AndReturn400WithError()
        {
            _controller.ModelState.AddModelError("test", "test");

            var result = _controller.UpdateLevel(new LevelDTO());
            var objectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

            [Fact]
        public void DeleteLevelId_ShouldCallService_AndReturn200()
        {

            _serviceMock.Setup(x => x.Delete(1));

            var result = _controller.DeleteLevelById(1);

            _serviceMock.Verify(x => x.Delete(1), Times.Once);

            var objectResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void DeleteLevelId_Shouldcallservice_Andreturn204()
        {
            var result = _controller.DeleteLevelById(null);

            _serviceMock.Verify(x => x.Delete(null), Times.Never);

            var objectresult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, objectresult.StatusCode);
        }
    }
}
