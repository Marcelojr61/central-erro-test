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

        public Guid ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Name { get; set; }

        public ICollection<Error> Errors { get; set; }
    }
}