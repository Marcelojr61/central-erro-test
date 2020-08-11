using System.ComponentModel.DataAnnotations;

namespace CentralDeErros.Transport
{
    public class MicrosserviceDTO
    {
        public int? Id { get; set; }

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

        public string Token { get; set; }
    }
}
