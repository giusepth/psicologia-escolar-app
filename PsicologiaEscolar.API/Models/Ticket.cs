using System.ComponentModel.DataAnnotations;

namespace PsicologiaEscolar.API.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int IdUsuario { get; set; }

        public string? Estado { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public string Descripcion { get; set; }

        public string? Respuesta { get; set; }

        public DateTime? FechaRespuesta { get; set; }

    }
}
