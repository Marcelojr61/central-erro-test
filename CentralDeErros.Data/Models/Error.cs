using System;

namespace CentralDeErros.Model.Models
{
    public class Error
    {
        public Error()
        {
            ErrorDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Origin { get; set; }
        public string Details { get; set; }
        public DateTime ErrorDate { get; set; }
        public bool IsArchived { get; set; }

        public Guid MicrosserviceClientId { get; set; }
        public Microsservice Microsservice { get; set; }

        public int EnviromentId { get; set; }
        public Environment Environment { get; set; }

        public int LevelId { get; set; }
        public Level Level { get; set; }
    }
}