using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CentralDeErros.Transport
{
    public  class UserUpdateDTO
    {
        [Required(ErrorMessage = "Informe um {0}")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Informe um {0}")]
        public string Email { get; set; }
        [Compare(nameof(Email), ErrorMessage = "Email não confere")]
        public string VerifiedEmail { get; set; }

    }
}
