using Contactos.Data;
using Contactos.Data.Entities;
using Contactos.Data.Repositories;

namespace Contactos.Data.Repositories
{
    public class ContactRepository: BaseRepository<Contact>
    {
        private DatabaseContext Context
        {
            get { return _context as DatabaseContext; }
        }

        public ContactRepository()
        {
            
        }

        public ContactRepository(DatabaseContext context) : base(context)
        {
           
        }

        
    }
}
