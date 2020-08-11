using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace CentralDeErros.API.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpiresHours { get; set; }
        public string Issuer { get; set; }
    }
}
