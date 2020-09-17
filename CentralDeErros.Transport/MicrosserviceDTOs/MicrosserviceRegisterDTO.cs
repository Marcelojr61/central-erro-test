using System;
using System.ComponentModel.DataAnnotations;

namespace CentralDeErros.Transport.MicrosserviceDTOs
{
    public class MicrosserviceRegisterDTO
    {
        public Guid ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Name { get; set; }
        
    }
}
