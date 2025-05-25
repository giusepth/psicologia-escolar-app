using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PsicologiaEscolar.API.Data;
using PsicologiaEscolar.API.Models;
namespace PsicologiaEscolar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] 
    public class TicketController : ControllerBase
    {

        private readonly TicketRepository _repository;

        public TicketController(TicketRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult CrearTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool creado = _repository.CrearTicket(ticket);

            if (creado)
            {
                return Ok(new { mensaje = "Ticket creado exitosamente." });
            }

            return StatusCode(500, new { mensaje = "Hubo un error al crear el ticket." });

        }

    }
        
}
