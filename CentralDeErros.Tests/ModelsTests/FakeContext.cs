using CentralDeErros.Model.Models;
using System.Collections.Generic;

namespace CentralDeErros.ModelsTests
{
    public class FakeContext 
    {
        public List<Level> Environments = new List<Level>()
        {
            new Level () { Id = -1, Name = "Teste" },
            new Level () { Id = 2, Name = "T" }
        };

        public List<Model.Models.Environment> Levels = new List<Model.Models.Environment>()
        {
            new Model.Models.Environment () { Id = -1, Phase = "Teste" }, 
            new Model.Models.Environment () { Id = 2, Phase = "T" }
        };
    }
}

