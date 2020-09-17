using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CentralDeErros.Services.Interfaces
{
    public interface ILevelService : IServiceBase<Level>
    {
        void Delete(int? id);
        Level RegisterOrUpdate(Level level);
    }
}
