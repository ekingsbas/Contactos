using System;
using System.Collections.Generic;
using System.Text;

namespace Contactos.Shrd.DTO.API
{
    public class ApiResultDTO
    {
        public string Message { get; set; } = string.Empty;
        public bool Error { get; set; }
    }
}
