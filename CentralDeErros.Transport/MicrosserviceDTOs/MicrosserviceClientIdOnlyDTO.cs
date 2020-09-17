using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CentralDeErros.Transport.MicrosserviceDTOs
{
    public class MicrosserviceClientIdOnlyDTO
    {
        [Required(ErrorMessage = "É obrigatório informar um ClientId")]
        public Guid ClientId { get; set; }
    }
}
