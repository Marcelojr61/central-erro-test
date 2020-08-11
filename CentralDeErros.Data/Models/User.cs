using System;
using System.ComponentModel.DataAnnotations;

namespace CentralDeErros.Model.Models
{
    public class User
    {
        public int Id { get; set; }

        [MinLength(3)]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage="A senha deve conter no mínimo 8 dígitos")]
        public string Password { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Apenas números positivos permitidos")]
        public int ProfileId { get; set; }
        
        public Profile Profile { get; set; }

    }
}