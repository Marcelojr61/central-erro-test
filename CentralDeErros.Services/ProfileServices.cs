using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Base;

namespace CentralDeErros.Services
{
    public class ProfileService : ServiceBase<Profile>
    {
        public ProfileService(CentralDeErrosDbContext context) : base(context)
        {

        }
    }
}