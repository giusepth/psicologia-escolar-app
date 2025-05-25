using Microsoft.AspNetCore.Mvc;
using PsicologiaEscolar.API.Data;
using PsicologiaEscolar.API.Models;

namespace PsicologiaEscolar.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
   public class AuthController : ControllerBase
   {
     private readonly UsuarioRepository _usuarioRepository; 

      public AuthController(UsuarioRepository usuarioRepository)
      {
            _usuarioRepository = usuarioRepository;

      }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var usuario = _usuarioRepository.ValidarCredenciales(request.Email, request.Password);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas"});
            }

            return Ok(new
            {
                usuario.IdUsuario,
                usuario.Nombre,
                usuario.Rol
            });
        }


   }
}

