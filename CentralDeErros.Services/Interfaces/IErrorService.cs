using CentralDeErros.Model.Models;
using CentralDeErros.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Services.Interfaces
{
    public interface IErrorService : IServiceBase<Error>
    {
        
        ICollection<Error> List(int? start, int? end, bool archived);

        ICollection<Error> SearchByEnviroment(int enviromentId);
        
        ICollection<Error> SearchByErrorLevel(string errorLevel);

        ICollection<Error> SearchByMicrosservice(Guid clientId);

        ICollection<Error> SearchByDate(DateTime start, DateTime end);
        new Error Register(Error entry);

        List<int> ArchiveById(List<int> errorIdList);

        bool CheckError(int id);
    }
}
