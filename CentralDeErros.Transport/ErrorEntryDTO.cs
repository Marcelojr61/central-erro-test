using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;

namespace CentralDeErros.Transport
{
    public class ErrorEntryDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        public string Origin { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        public string Details { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} is Required")]
        public int MicrosserviceId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} is Required")]
        public int EnviromentId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} is Required")]
        public int LevelId { get; set; }
    }
}
