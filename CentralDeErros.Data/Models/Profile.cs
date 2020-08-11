using System;
using System.Collections.Generic;

namespace CentralDeErros.Model.Models
{
    public class Profile
    {
        public Profile()
        {
            this.Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<User> Users { get; set; }
        
    }
}
