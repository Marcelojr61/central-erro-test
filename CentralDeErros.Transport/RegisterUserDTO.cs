using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CentralDeErros.Transport
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Informe um {0}")]
        [EmailAddress(ErrorMessage = "Informe um {0} válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe um {0}")]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Senhas não conferem") ]
        public string ConfirmPassword { get; set; }
    }
}
