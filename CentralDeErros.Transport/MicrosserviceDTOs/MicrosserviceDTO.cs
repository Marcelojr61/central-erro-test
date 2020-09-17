using System;
using System.ComponentModel.DataAnnotations;

namespace CentralDeErros.Transport.MicrosserviceDTOs
{
    public class MicrosserviceDTO
    {
        public Guid ClientId { get; set; }
        private string _name;
        [Required(ErrorMessage = "É obrigatório informar um microsservice", AllowEmptyStrings = false)]
        public string Name
        {
            get => _name;
            set
            {
                _name = value.ToLower();
            }
        }
    }
}
