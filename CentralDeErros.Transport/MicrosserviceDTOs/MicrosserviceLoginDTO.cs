using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CentralDeErros.Transport.MicrosserviceDTOs
{
    public class MicrosserviceLoginDTO
    {
        [Required(ErrorMessage = "É necessário informar um {0}")]
        public Guid ClientId { get; set; }
        [Required(ErrorMessage = "É necessário informar um {0}")]
        public String ClientSecret { get; set; }
    }
}
