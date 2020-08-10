using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace CentralDeErros.Core.Extensions
{
    public class AppSettingsJWT
    {
        public string Secret { get; set; }

        public string Emissor { get; set; }
    }
}
 