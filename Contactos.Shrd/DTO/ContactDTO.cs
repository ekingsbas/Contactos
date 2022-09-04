using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Shrd.DTO
{
    public class ContactDTO: BaseDTO
    {
        
        public string Nombre { get; set; }

        
        public string Telefono { get; set; }

        
        public string Email { get; set; }
    }
}
