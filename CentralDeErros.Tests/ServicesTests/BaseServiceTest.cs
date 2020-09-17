using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CentralDeErros.ServicesTests
{
    public class BaseServiceTest
    {


        [Fact]
        public void List_Should_Return_All_Values()
        {
            // Arrange
            using var fakeContext = new FakeContext("ListValues");
            fakeContext.FillWithAll();
            var service = new ServiceBase<Error>(fakeContext.context);
            var expected = fakeContext.GetFakeData<Error>();

            // Act
            var result = service.List();

            // Assert
            Assert.IsAssignableFrom<IQueryable<Error>>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Count(), expected.Count());
        }

        [Fact]
        public void List_With_Filter_Should_Return_Right_Values()
        {
            // Arrange
            var fakeContext = new FakeContext("ListFilter");
            fakeContext.FillWithAll();
            var service = new ServiceBase<Error>(fakeContext.context);
            var expected = fakeContext.GetFakeData<Error>()
                .Where(x => x.IsArchived == true);

            // Act
            var result = service.List(x => x.IsArchived == true);

            // Assert
            Assert.IsAssignableFrom<IQueryable<Error>>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Count(), expected.Count());
        }

        [Fact]
        public void Fetch_Should_Return_Correct_Value()
        {
            int Id = 1;

            // Arrange
            var fakeContext = new FakeContext("Fetch");
            fakeContext.FillWithAll();
            var service = new ServiceBase<Error>(fakeContext.context);
            var expected = fakeContext.GetFakeData<Error>()
                .Where(x => x.Id == Id);

            // Act
            var result = service.Fetch(Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Error>(result);
            Assert.Equal(result.Id, Id);
        }

        [Fact]
        public void Fetch_With_Filter_Should_Return_Right_Value()
        {
            string detail = "Detail1";

            // Arrange
            var fakeContext = new FakeContext("FetchWithFilter");
            fakeContext.FillWithAll();
            var service = new ServiceBase<Error>(fakeContext.context);
            var expected = new Error
            {

                Id = 1,
                Title = "Teste1",
                Origin = "1.0.0.1",
                Details = "Detail1",
                ErrorDate = DateTime.Parse("2020-05-01 21:15:33"),
                MicrosserviceClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"),
                EnviromentId = 1,
                LevelId = 1,
                IsArchived = false

            };

            // Act
            var result = service.Fetch(x => x.Details == detail);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Error>(result);
            Assert.Equal(result.Id, expected.Id);
        }

        [Fact]
        public void Register_Should_Register_One_Value()
        {
            // Arrange
            var fakeContext = new FakeContext("Register");
            fakeContext.FillWith<Level>();
            fakeContext.FillWith<Model.Models.Environment>();
            fakeContext.FillWith<Microsservice>();

            var service = new ServiceBase<Error>(fakeContext.context);

            var entry = new Error
            {
                Title = "entry",
                Origin = "entry@email.com",
                Details = "detailEntry",
                MicrosserviceClientId = new Guid("84cd83de-1809-4f3d-a92c-b263d18f4244"),
                EnviromentId = 1,
                LevelId = 1,
                IsArchived = false
            };

            // Act
            service.Register(entry);
            var result = fakeContext.GetFakeData<Error>().FirstOrDefault(x => x.Id == 1);
            // Assert
            Assert.NotNull(result);
            Assert.True(fakeContext.context.Errors.Count() == 1, "Should has one Error inside dbContext");
            Assert.True(fakeContext.GetFakeData<Error>().Any(x => x.Id == 1), "Should has entry with Id 1");
        }

        [Fact]
        public void Update_Should_Update_Successfully()
        {
            // Arrange
            var fakeContext = new FakeContext("UpdateBase");
            fakeContext.FillWithAll();
            var service = new ServiceBase<Error>(fakeContext.context);

            var entry = new Error
            {
                Id = 1,
                Title = "Updated",
                Origin = "updated@email.com",
                Details = "update",
                MicrosserviceClientId = new Guid("84cd83de-1809-4f3d-a92c-b263d18f4244"),
                EnviromentId = 1,
                LevelId = 1,
                IsArchived = true
            };

            // Act
            service.Update(entry);
            var result = fakeContext.context.Errors
                .FirstOrDefault(x => x.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entry.Title, result.Title);
        }

        [Fact]
        public void Delete_Should_Delete_The_Correct_Register()
        {
            // Arrange
            var fakeContext = new FakeContext("DeleteBase");
            fakeContext.FillWithAll();
            var service = new ServiceBase<Error>(fakeContext.context);

            var entry = fakeContext.GetFakeData<Error>().FirstOrDefault();

            // Act
            service.Delete(entry);
            var result = fakeContext.context.Errors
                .FirstOrDefault(x => x.Id == 1);

            // Assert
            Assert.Null(result);

        }

        [Fact]
        public void Delete_Should_Delete_By_Id()
        {
            // Arrange
            var fakeContext = new FakeContext("Delete");
            fakeContext.FillWithAll();
            var service = new ServiceBase<Error>(fakeContext.context);

            // Act
            service.Delete(1);
            var result = fakeContext.context.Errors
                .FirstOrDefault(x => x.Id == 1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Dispose_Should_Set_Context_To_Null()
        {
            // Arrange
            var fakeContext = new FakeContext("Update");
            var service = new ServiceBase<Error>(fakeContext.context);

            // Act
            service.Dispose();

            // Assert
            Assert.Null(service.Context);
        }
    }
}
