using Microsoft.AspNetCore.Diagnostics;

namespace WebAPI.Controllers
{
    public class ControladorDeErrores : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            Exception? excepcion = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            return Problem();
        }
    }
}
