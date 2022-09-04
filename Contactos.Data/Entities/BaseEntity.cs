using System.ComponentModel.DataAnnotations;

namespace Contactos.Data.Entities
{
    public class BaseEntity
    {
        //[Column(Name = "ID", IsDbGenerated = true, IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int ID { get; set; }
    }
}
