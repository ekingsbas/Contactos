using Contactos.Data.Entities;
using Contactos.Data.Repositories;
using Contactos.Shrd.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Business.Services
{
    
    public class ContactosService
    {
        private readonly ContactRepository _repository;

        public ContactosService()
        {

        }

        public ContactosService(ContactRepository contactRepository) //Inyectando dependencias
        {
            _repository = contactRepository;
        }

        public async Task<ContactDTO> GetById(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                var resultDto = new ContactDTO
                {
                    Id = result.ID,
                    Nombre = result.ContactName,
                    Telefono = result.Phone,
                    Email = result.Email
                };
                return resultDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ContactDTO>> GetAll()
        {
            try
            {
                var result = await _repository.GetAllAsync();
                List<ContactDTO> returns = null;

                if (result?.Count() > 0)
                {
                    returns = result
                        .Select(e => 
                            new ContactDTO
                            {
                                Id = e.ID,
                                Nombre = e.ContactName,
                                Telefono = e.Phone,
                                Email = e.Email
                            }
                        ).ToList();
                }

                return returns;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ContactDTO> Create(ContactDTO model)
        {
            try
            {
                var newContact = new Contact
                {
                    ContactName = model.Nombre,
                    Phone = model.Telefono,
                    Email = model.Email
                };

                var result = _repository.Create(newContact);
                await _repository.Commit();

                model.Id = result.ID;

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(ContactDTO dto)
        {
            try
            {
                var currentContact = await _repository.GetByIdAsync((int)dto.Id);
                if (currentContact == null)
                {
                    throw new Exception($"El Contacto con Id {dto.Id} no se encontró, no se puede actualizar.");
                }

                currentContact.ContactName = dto.Nombre;
                currentContact.Phone = dto.Telefono;
                currentContact.Email = dto.Email;
                
                var updateResult = _repository.Update(currentContact);
                await _repository.Commit();

                return updateResult != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var currentContact = await _repository.GetByIdAsync(id);
                if (currentContact == null)
                {
                    throw new Exception($"El Contacto con el  {id} no se encontró, no se puede eliminar.");
                }

                _repository.Delete(currentContact);
                await _repository.Commit();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
