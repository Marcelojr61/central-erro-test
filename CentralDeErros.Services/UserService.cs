using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public class UserService : ServiceBase<User>
    {
        public UserService(CentralDeErrosDbContext context) : base(context)
        {

        }

    }
}
