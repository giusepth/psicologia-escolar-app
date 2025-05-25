using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PsicologiaEscolar.API.Filters
{
    public class AutenticadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var usuarioId = session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                context.Result = new UnauthorizedObjectResult(new { mensaje = "No estás autenticado" });
            }

            base.OnActionExecuting(context);
        }
    }
}