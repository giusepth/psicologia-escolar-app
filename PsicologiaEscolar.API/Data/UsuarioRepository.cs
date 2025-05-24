using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PsicologiaEscolar.API.Models;
namespace PsicologiaEscolar.API.Data
{ // Con esta clase buscamos utilizar los procedimientos almacenados que tenemos en la base de datos para la tabla usuario, el cual es para validar si el usuario existe
    public class UsuarioRepository
    {
        private readonly string _connectionString; //Declaramos una variable por la cual estableceremos la cadena de conexion 

        public UsuarioRepository(IConfiguration configuration)
        { // Establecemos la conexion accediendo al archivo appsetting.json donde ya hemos declarado la cadena de conexion
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Usuario? ValidarCredenciales(string email,  string password)
        {//Este metodo realiza la validación de los parametros ingresados y verifica que coincidan con los establecidos en la base de datos

            using (SqlConnection conn = new SqlConnection(_connectionString)) 
                // crea la conexion usando _connecitonString (que esta establecida en el appseting.json
            {
                using (SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; //crea un comando donde indicamos que vamos a utilizar un procedimiento almacenado 

                    //Aqui se reciben los parametros que se envian al procedimiento
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", password);

                    //Abrimos la conexion y ejecutamos

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {// Si se encuentra una coincidencia, crea un nuevo objeto Usuario, y le asigna los valores encontrados en la base de datos.
                            return  new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Email = reader["Email"].ToString(),
                                Rol = reader["Rol"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString()
                            };
                        }
                              
                    }

                }
            }
            // Si no encuentra usuario retornamos null
            return null;
        }


    }
}
