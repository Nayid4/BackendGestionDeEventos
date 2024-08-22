using Application.Eventos.Actualizar;
using Application.Eventos.AgregarAsistente;
using Application.Eventos.Crear;
using Application.Eventos.Eliminar;
using Application.Eventos.EliminarAsistente;
using Application.Eventos.ListarPorId;
using Application.Eventos.ListarTodos;
using Application.Usuarios.Actualizar;
using Application.Usuarios.Crear;
using Application.Usuarios.Eliminar;
using Application.Usuarios.ListarPorId;
using Application.Usuarios.ListarTodos;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("eventos")]
    public class ControladorDeEventos : ControladorDeAPI
    {
        private readonly ISender _mediator;

        public ControladorDeEventos(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var resultadosDeListarTodos = await _mediator.Send(new ConsultaListarTodosLosEventos());

            return resultadosDeListarTodos.Match(
                eventos => Ok(eventos),
                errores => Problem(errores)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var resultadoDeListarPorId = await _mediator.Send(new ConsultaListarPorIdEvento(id));

            return resultadoDeListarPorId.Match(
                evento => Ok(evento),
                errores => Problem(errores)
            );
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] ComandoCrearEvento comando)
        {
            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                eventoId => Ok(eventoId),
                errores => Problem(errores)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ComandoActualizarEvento comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("Evento.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActualizar = await _mediator.Send(comando);

            return resultadoDeActualizar.Match(
                eventoId => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var resultadoDeEliminar = await _mediator.Send(new ComandoEliminarEvento(id));

            return resultadoDeEliminar.Match(
                eventoId => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPost("asistentes")]
        public async Task<IActionResult> AgregarAsistente([FromBody] ComandoAgregarAsistenteDeEvento comando)
        {
            var resultadoDeAgregar = await _mediator.Send(comando);

            return resultadoDeAgregar.Match(
                eventoId => Ok(eventoId),
                errores => Problem(errores)
            );
        }

        [HttpDelete("asistentes")]
        public async Task<IActionResult> EliminarAsistente([FromBody] ComandoEliminarAsistenteDeEvento comando)
        {
            var resultadoDeEliminar = await _mediator.Send(comando);

            return resultadoDeEliminar.Match(
                eventoId => NoContent(),
                errores => Problem(errores)
            );
        }

    }
}
