using CentralDeErros.Model.Models;
using CentralDeErros.Services.Base;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CentralDeErros.Services.Interfaces
{
    public interface IMicrosserviceService : IServiceBase<Microsservice>
    {
        void Delete(Guid? clientId);
        Microsservice Register(string name);
        Microsservice Fetch(Guid id);
        Microsservice GenerateClientSecret(Microsservice microsservice);
        bool ValidateMicrosserviceCredentials(Microsservice microsservice);
    }
}
