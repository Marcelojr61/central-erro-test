using CentralDeErros.Model.Models;
using System;
using System.Collections.Generic;

namespace CentralDeErros.ControllersTests
{
    public class FakeContext 
    {
        public static List<Error> Errors = new List<Error>()
        {
            new Error () { 
                Id = 1,
                Title = "Teste1",
                Origin  = "Teste1@email.com",
                Details = "Detail1",
                MicrosserviceClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"),
                EnviromentId = 1,
                LevelId = 1,
                IsArchived = false
            },
            new Error () { 
                Id = 2,
                Title = "Teste2",
                Origin  = "Teste2@email.com",
                Details = "Detail2",
                MicrosserviceClientId = new Guid("3691b3e5-c411-4fd2-93d4-11a081552fb3"),
                EnviromentId = 1,
                LevelId = 1,
                IsArchived = false
            },
            new Error () { 
                Id = 3, 
                Title = "Teste3", 
                Origin  = "Teste3@email.com", 
                Details = "Detail3",
                MicrosserviceClientId = new Guid("87b16267-2062-49bc-9872-ef3aa1d21e21"), 
                EnviromentId = 1, 
                LevelId = 1, 
                IsArchived = false
            },
            new Error () { 
                Id = 4, 
                Title = "Teste4", 
                Origin  = "Teste4@email.com", 
                Details = "Detail4",
                MicrosserviceClientId = new Guid("2b49c2d5-3b93-4568-ab1b-6399fb0ba3ae"), 
                EnviromentId = 1, 
                LevelId = 1, 
                IsArchived = false
            },
            new Error () { 
                Id = 5, 
                Title = "Teste5", 
                Origin  = "Teste5@email.com", 
                Details = "Detail5",
                MicrosserviceClientId = new Guid("65d65c94-9de0-4f5a-89de-6abd6309c9cc"), 
                EnviromentId = 1, 
                LevelId = 1, 
                IsArchived = false
            },
        };

        public static List<Level> Environments = new List<Level>()
        {
            new Level () { Id = 1, Name = "Teste" },
            new Level () { Id = 2, Name = "T" }
        };

        public static List<Model.Models.Environment> Levels = new List<Model.Models.Environment>()
        {
            new Model.Models.Environment () { Id = 1, Phase = "Teste" }, 
            new Model.Models.Environment () { Id = 2, Phase = "T" }
        };

         public static List<Microsservice> Microsservices = new List<Microsservice>()
        {
            new Microsservice () { ClientId = new Guid("031c156c-c072-4793-a542-4d20840b8031"), Name = "Microsservice1", ClientSecret = "c4ca4238a0b923820dcc509a6f75849b" },
            new Microsservice () { ClientId = new Guid("3691b3e5-c411-4fd2-93d4-11a081552fb3"), Name = "Microsservice2", ClientSecret = "c81e728d9d4c2f636f067f89cc14862c" },
            new Microsservice () { ClientId = new Guid("87b16267-2062-49bc-9872-ef3aa1d21e21"), Name = "Microsservice3", ClientSecret = "eccbc87e4b5ce2fe28308fd9f2a7baf3" }, 
            new Microsservice () { ClientId = new Guid("2b49c2d5-3b93-4568-ab1b-6399fb0ba3ae"), Name = "Microsservice4", ClientSecret = "a87ff679a2f3e71d9181a67b7542122c" },
            new Microsservice () { ClientId = new Guid("65d65c94-9de0-4f5a-89de-6abd6309c9cc"), Name = "Microsservice5", ClientSecret = "e4da3b7fbbce2345d7772b0674a318d5" }
        };

    }
}

