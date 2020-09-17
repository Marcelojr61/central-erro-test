using CentralDeErros.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeErros.ModelsTests
{
    public class LevelModelTests : BaseModelTest
    {
        public LevelModelTests()
        {
        }

        [Fact(DisplayName = "Levels shouldn't be null")]
        public void LevelsShouldNotBeNull()
        {
            var levels = context.Levels.ToList();

            foreach (var level in levels) Assert.NotNull(level);

        }
    }
}
