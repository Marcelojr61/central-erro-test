using CentralDeErros.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Services.Interfaces
{
    public interface IEnvironmentService : IServiceBase<Model.Models.Environment>
    {
        void Delete(int? id);
        public Model.Models.Environment RegisterOrUpdate(Model.Models.Environment environment);
    }
}
