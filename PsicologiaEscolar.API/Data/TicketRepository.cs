using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PsicologiaEscolar.API.Models;
namespace PsicologiaEscolar.API.Data
{
    public class TicketRepository
    {
        private readonly string _connectionString;

        public TicketRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool CrearTicket(Ticket ticket)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_CrearTicket", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", ticket.IdUsuario);
                    cmd.Parameters.AddWithValue("@Descripcion", ticket.Descripcion);

                    int filasAfectadas = cmd.ExecuteNonQuery(); // ← Devuelve el número de filas afectadas

                    return filasAfectadas > 0; // ← Retorna true si al menos una fila fue insertada
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Ticket> ObtenerTicketsPorUsuario (int IdUsuario)
        {
            var tickets = new List<Ticket>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerTicketsPorUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ticket = new Ticket()
                            {
                                TicketId = Convert.ToInt32(reader["TicketId"]),
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Estado = reader["Estado"].ToString(),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                Descripcion = reader["Descripcion"].ToString(),
                                Respuesta = reader["Respuesta"] == DBNull.Value ? null : reader["Respuesta"].ToString(),
                                FechaRespuesta = reader["FechaRespuesta"] == DBNull.Value ? null : Convert.ToDateTime(reader["FechaRespuesta"])
                            };

                            tickets.Add(ticket);
                        }
                    }
                }
            }
            return tickets;


        }

        public List<Ticket> ObtenerTodosLosTickets()
        {

            var tickets = new List<Ticket>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerTodosTickets", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ticket = new Ticket()
                            {
                                TicketId = Convert.ToInt32(reader["TicketId"]),
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Estado = reader["Estado"].ToString(),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                Descripcion = reader["Descripcion"].ToString(),
                                Respuesta = reader["Respuesta"] == DBNull.Value ? null : reader["Respuesta"].ToString(),
                                FechaRespuesta = reader["FechaRespuesta"] == DBNull.Value ? null : Convert.ToDateTime(reader["FechaRespuesta"])
                            };
                            tickets.Add(ticket);
                        
                        }
                    }
                     
                }
            }
            return tickets;


        }

        public bool ActualizarTicket(Ticket ticket)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarTicket", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TicketId", ticket.TicketId);
                    cmd.Parameters.AddWithValue("@Estado", ticket.Estado);
                    cmd.Parameters.AddWithValue("@Respuesta", string.IsNullOrEmpty(ticket.Respuesta) ? DBNull.Value : ticket.Respuesta);
                    cmd.Parameters.AddWithValue("@FechaRespuesta", ticket.FechaRespuesta ?? (object)DBNull.Value);

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }

           
        }

        public bool EliminarTicket(int ticketId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_EliminarTicket", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TicketId", ticketId);

                    connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
        }
    }
}
