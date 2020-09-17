using CentralDeErros.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeErros.ModelsTests
{
    public class EnvironmentModelTests : BaseModelTest
    {
        public EnvironmentModelTests()
        {
        }

        [Fact(DisplayName = "Environments shouldn't be null")]
        public void EnvironmentShouldNotBeNull()
        {
            var environments = context.Environments.ToList();

            foreach (var environment in environments) Assert.NotNull(environment);
        }
    }
}