using System.ComponentModel.DataAnnotations;

namespace CentralDeErros.Transport
{
    public class EnvironmentDTO
    {
        public int? Id { get; set; }

        private string _phase;
        [Required(ErrorMessage = "É obrigatório informar o nome do ambiente", AllowEmptyStrings = false)]
        public string Phase
        {
            get => _phase;
            set
            {
                _phase = value.ToLower();
            }
        }
    }
}
