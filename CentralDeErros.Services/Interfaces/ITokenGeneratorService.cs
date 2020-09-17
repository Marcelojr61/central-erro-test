using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeErros.Services.Interfaces
{
    public interface ITokenGeneratorService
    {
        Task<string> TokenGenerator(string username);
        Object TokenGeneratorMicrosservice(string clientId);

    }
}
