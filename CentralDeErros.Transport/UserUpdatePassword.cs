using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CentralDeErros.Transport
{
    public class UserUpdatePassword
    {
        [Required(ErrorMessage = "Informe um {0}")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe um {0}")]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
}
