using System;
using System.Collections.Generic;

namespace CentralDeErros.Model.Models
{
    public class Microsservice
    {
        public Microsservice()
        {
            Errors = new HashSet<Error>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public ICollection<Error> Errors { get; set; }
    }
}
