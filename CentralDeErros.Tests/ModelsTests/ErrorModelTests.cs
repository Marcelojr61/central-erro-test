using CentralDeErros.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeErros.ModelsTests
{
    public class ErrorModelTests : BaseModelTest
    {
        public ErrorModelTests(CentralDeErrosDbContext context) : base(context)
        {          
        }

        [Fact(DisplayName = "Error should not be null")]
        public void ErrorsShouldNotBeNull()
        {
            var errors = context.Errors.ToList();

            foreach (var error in errors) Assert.NotNull(error);
        }
    }
}
