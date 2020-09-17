using CentralDeErros.Core;
using CentralDeErros.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace CentralDeErros.ServicesTests
{
    public class EnvironmentServiceTests
    {
        
        [Fact]
        public void Delete_Should_Remove_The_Right_Environment()
        {
            // Arrange
            using var fakeContext = new FakeContext("deleteEnvironment");
            fakeContext.FillWith<Model.Models.Environment>();

            var context = new CentralDeErrosDbContext(fakeContext.FakeOptions);
            var service = new EnvironmentService(context);

            // Act
            service.Delete(1);

            // Assert
            Assert.Null(context.Environments.Find(1));


        }


        [Fact]
        public void Delete_Should_Throw_An_Exception_When_Id_Is_Null()
        {
            // Arrange
            using var fakeContext = new FakeContext("deleteNullEnvironment");
            fakeContext.FillWithAll();

            var service = new EnvironmentService(fakeContext.context);

            // Act
            // Assert
            Assert.Throws<Exception>(() => service.Delete(null));


        }

        [Fact]
        public void Delete_Should_Throw_An_Exception_When_Try_To_Delete_An_In_Use_Environment()
        {
            // Arrange
            using var fakeContext = new FakeContext("deleteEnvironmentInUse");
            fakeContext.FillWithAll();
            var service = new EnvironmentService(fakeContext.context);

            // Act
            // Assert
            Assert.Throws<Exception>(() => service.Delete(1));


        }

        [Fact]
        public void RegisterOrUpdate_Should_Add_When_No_Id()
        {
            // Arrange
            using var fakeContext = new FakeContext("RegisterEnvironment");
            var service = new EnvironmentService(fakeContext.context);
            var entry = new Model.Models.Environment
            {
                Phase = "TestNewRegister"
            };
            // Act
            var result = service.RegisterOrUpdate(entry);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, fakeContext.context.Environments.Count());
            Assert.True(result.Id == 1, "Id Should be 1");

        }

        [Fact]
        public void RegisterOrUpdate_Should_Update_When_Id_Is_Passed()
        {
            // Arrange
            using var fakeContext = new FakeContext("Update");
            fakeContext.GenerateContext();
            var service = new EnvironmentService(fakeContext.context);

            var old = fakeContext.GetFakeData<Model.Models.Environment>()
                .FirstOrDefault(x => x.Id == 1);

            var entry = new Model.Models.Environment
            {
                Id = 1,
                Phase = "UpdatedTest"
            };

            // Act
            var result = service.RegisterOrUpdate(entry);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(old.Phase, fakeContext.context.Environments.Find(1).Phase);

        }

    }
}