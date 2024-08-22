using Application.Usuarios.Actualizar;
using Application.Usuarios.Crear;
using Application.Usuarios.Eliminar;
using Application.Usuarios.ListarPorId;
using Application.Usuarios.ListarTodos;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("usuarios")]
    public class ControladorDeUsuarios : ControladorDeAPI
    {
        private readonly ISender _mediator;

        public ControladorDeUsuarios(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        //[Authorize(Roles = ("Usuario"))]
        public async Task<IActionResult> ListarTodos()
        {
            var resultadosDeListarTodos = await _mediator.Send(new ConsultaListarTodosLosUsuarios());

            return resultadosDeListarTodos.Match(
                usuarios => Ok(usuarios),
                errores => Problem(errores)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var resultadoDeListarPorId = await _mediator.Send(new ConsultaListarPorIdUsuario(id));

            return resultadoDeListarPorId.Match(
                usuario => Ok(usuario),
                errores => Problem(errores)
            );
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] ComandoCrearUsuario comando)
        {
            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                usuarioId => Ok(usuarioId),
                errores => Problem(errores)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ComandoActualizarUsuario comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("Usuario.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActualizar = await _mediator.Send(comando);

            return resultadoDeActualizar.Match(
                usuarioId => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var resultadoDeEliminar = await _mediator.Send(new ComandoEliminarUsuario(id));

            return resultadoDeEliminar.Match(
                usuarioId => NoContent(),
                errores => Problem(errores)
            );
        }
    }
}
