using Contactos.Business.Services;
using Contactos.Shrd.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Contactos.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactosController : ControllerBase
    {
        private ContactosService _service;

        public ContactosController(ContactosService service)
        {
            _service = service;
        }
        // GET: Contactos
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var result = await _service.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: Contactos/Details/5
        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var result = await _service.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: Contactos/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactDTO item)
        {
            if (item == null)
                return BadRequest();

            var result = await _service.Create(item);

            return Created("", result);
        }


        // PUT: Contactos/Update/5
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContactDTO item)
        {
            if (item == null)
                return BadRequest();

            var result = await _service.Update(item);

            return Ok(new { error = !result, message = result ? "Contacto actualizado" : "No se pudo actualizar el contacto" });
        }



        // DELETE: Contactos/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var result = await _service.Delete(id);

            return Ok(new { error = !result, message = result ? "Contacto eliminado" : "No se pudo eliminar el contacto" });
        }

        
    }
}
