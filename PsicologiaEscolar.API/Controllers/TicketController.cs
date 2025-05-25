using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PsicologiaEscolar.API.Data;
using PsicologiaEscolar.API.Models;
using PsicologiaEscolar.API.Filters;
namespace PsicologiaEscolar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Autenticado] 
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

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult ObtenerTicketsPorUsuario(int idUsuario)
        {
            var tickets = _repository.ObtenerTicketsPorUsuario(idUsuario);

            if (tickets == null || tickets.Count == 0)
            {
                return NotFound(new { mensaje = "No se encontraron tickets para este usuario" });
            }

            return Ok(tickets);
        }

        [HttpGet("AllTickets")]
        public IActionResult ObtenerTodosLosTickets()
        {
            var tickets = _repository.ObtenerTodosLosTickets();

            if (tickets == null || tickets.Count == 0)
            {
                return NotFound(new { mensaje = "No se encontraron tickets registrados." });
            }

            return Ok(tickets);
        }

        [HttpPut("actualizar")]
        public IActionResult ActualizarTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool actualizado = _repository.ActualizarTicket(ticket);

            if (actualizado)
            {
                return Ok(new { mensaje = "Ticket actualizado correctamente." });
            }

            return StatusCode(500, new { mensaje = "No se pudo actualizar el ticket." });
        }

        [HttpDelete("{ticketId}")]
        public IActionResult EliminarTicket(int ticketId)
        {
            bool eliminado = _repository.EliminarTicket(ticketId);

            if (eliminado)
            {
                return Ok(new { mensaje = "Ticket eliminado correctamente." });
            }
            return NotFound(new { mensaje = "No se encontró el ticket para eliminar." });
        }
    }
        
}
