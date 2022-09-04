using System.ComponentModel.DataAnnotations;

namespace Contactos.Data.Entities
{
    //[Table(Name = "Contact")]
    public class Contact: BaseEntity
    {
        

       
        public string ContactName { get; set; }

        
        public string Phone { get; set; }

        
        public string Email { get; set; }
    }
}
