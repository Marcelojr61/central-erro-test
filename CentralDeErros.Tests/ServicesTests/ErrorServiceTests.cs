using Castle.DynamicProxy.Generators;
using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using CentralDeErros.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeErros.ServicesTests
{
    public class ErrorServiceTests
    {
        private CentralDeErrosDbContext GenerateContext(string contextName)
        {
            var fakeContext = new FakeContext(contextName);
            fakeContext.FillWithAll();

            return new CentralDeErrosDbContext(fakeContext.FakeOptions);
        }


        [Theory]
        [InlineData(null, null, false)]
        [InlineData(2, 2, false)]
        [InlineData(null, 5, false)]
        [InlineData(null, null, true)]
        public void List_Return_ShouldNotBeNull(
            int? start,
            int? end,
            bool archived)
        {
            // Arrange

            var context = GenerateContext("ListErros");
            var service = new ErrorService(context);

            var expected = context.Errors
                .Where(x => x.IsArchived == archived)
                .Skip(start.HasValue ? start.Value : 0)
                .ToList();

            // Act
            if (end.HasValue)
            {
                expected = expected
                    .Take(end.Value)
                    .ToList();
            }

            var result = service.List(
                start,
                end,
                archived);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Count(), expected.Count());

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(20)]
        public void SearchByEnviroment_Shold_Return_Right_Values(int enviromentId)
        {
            // Arrange
            var context = GenerateContext("SearchByEnviroment");
            var service = new ErrorService(context);

            var expected = context.Errors
            .Where(x => x.EnviromentId == enviromentId)
            .ToList();

            // Act
            var result = service.SearchByEnviroment(
               enviromentId);

            // Assert
            Assert.Equal(result.Count, expected.Count);


        }


        [Theory]
        [InlineData("warning")]
        [InlineData("debug")]
        [InlineData("critical")]
        public void SearchByErrorLevel_Shold_Return_Right_Values(string errorLevel)
        {
            // Arrange
            var context = GenerateContext("SearchByLevel");
            var service = new ErrorService(context);

            var expected = context.Errors
            .Where(x => x.Level.Name == errorLevel)
            .ToList();

            // Act
            var result = service.SearchByErrorLevel(
               errorLevel);

            // Assert
            Assert.Equal(result.Count, expected.Count);


        }

        [Theory]
        [InlineData("2020-05-01 21:15:33", "2020-05-05 21:15:33")]
        [InlineData("2020-05-01 21:15:33", "2020-12-05 21:15:33")]
        [InlineData("2020-06-01 21:15:33", "2020-12-01 21:15:33")]
        public void SearchByDate_Should_Return_Right_Value(DateTime start, DateTime end)
        {
            // Arrange
            var context = GenerateContext("SearchByDate");
            var service = new ErrorService(context);


            var expected = context.Errors
            .Where(x => x.ErrorDate >= start && x.ErrorDate <= end)
            .ToList();

            // Act
            var result = service.SearchByDate(start, end);

            // Assert
            Assert.Equal(expected.Count, expected.Count);

        }

        [Fact]
        public void Register_Should_Add_New_Error()
        {
            // Arrange
            var fakeContext = new FakeContext("RegisterError");
            fakeContext.FillWith<Level>();
            fakeContext.FillWith<Microsservice>();
            fakeContext.FillWith<Model.Models.Environment>();

            var context = new CentralDeErrosDbContext(fakeContext.FakeOptions);
            var service = new ErrorService(context);
            Error entry = new Error
            {
                Title = "Teste1",
                Origin = "1.0.0.1",
                Details = "Detail1",
                ErrorDate = DateTime.Today,
                MicrosserviceClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"),
                EnviromentId = 1,
                LevelId = 1,
                IsArchived = false
            };

            // Act
            var result = service.Register(entry);

            // Assert
            Assert.True(result.Id == 1, $"Should return new error with id 1. returned: {result.Id}");
            Assert.True(context.Errors.Count() == 1, $"Should have one error saved. > Amount: {context.Errors.Count()}");
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", 1, 1)]
        [InlineData("031c156c-c072-4793-a542-4d20840b8031", 20, 1)]
        [InlineData("031c156c-c072-4793-a542-4d20840b8031", 1, 20)]
        public void Register_Should_Throw_When_Add_Non_Existent_FK(
            Guid microsserviceId,
            int LevelId,
            int environmentId)
        {
            // Arrange
            var context = GenerateContext("RegisterBadError");
            var service = new ErrorService(context);

            Error entry = new Error
            {
                Id = 1,
                Title = "Teste1",
                Origin = "1.0.0.1",
                Details = "Detail1",
                ErrorDate = DateTime.Today,
                MicrosserviceClientId = microsserviceId,
                EnviromentId = environmentId,
                LevelId = LevelId,
                IsArchived = false
            };

            // Act
            // Assert
            Assert.Throws<Exception>(() => service.Register(entry));
        }

        [Fact]
        public void ArchiveById_Should_Archive_The_Correct_Amount()
        {
            // Arrange
            List<int> errors = new List<int> { 1, 2, 3, 20 };
            var context = GenerateContext("ArchiveError");
            var service = new ErrorService(context);

            // Act
            var result = service.ArchiveById(errors);

            // Assert
            Assert.NotNull(result);
            Assert.All(context.Errors.Where(x => errors.Contains(x.Id) ), x => Assert.True(x.IsArchived));
            Assert.True(result.Count == 1); // should return one error
        }

    }
}
