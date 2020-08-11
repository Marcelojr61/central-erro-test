using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CentralDeErros.Transport
{
    public class LevelDTO
    {
        public int? Id { get; set; }

        private string _name;

        [Required(ErrorMessage = "É obrigatório informar o tipo de erro", AllowEmptyStrings = false)]
        public string Name
        {
            get => _name;
            set {
                _name = value.ToLower();
            }
        }
    }
}
