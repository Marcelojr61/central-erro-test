using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Transport
{
    public class ErrorDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Origin { get; set; }
        public string Details { get; set; }
        public DateTime ErrorDate { get; set; }

        public int MicrosserviceId { get; set; }

        public int EnviromentId { get; set; }

        public int LevelId { get; set; }
    }
}
