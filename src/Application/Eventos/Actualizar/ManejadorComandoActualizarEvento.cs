using Domain.AsistentesDeEventos;
using Domain.Eventos;
using Domain.Primitivos;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Eventos.Actualizar
{
    public sealed class ManejadorComandoActualizarEvento : IRequestHandler<ComandoActualizarEvento, ErrorOr<Unit>>
    {
        private readonly IRepositorioEvento _repositorioEvento;
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IUnitOfWork _unitOfWork;

        public ManejadorComandoActualizarEvento(IRepositorioEvento repositorioEvento, IRepositorioUsuario repositorioUsuario, IUnitOfWork unitOfWork)
        {
            _repositorioEvento = repositorioEvento ?? throw new ArgumentNullException(nameof(repositorioEvento));
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ComandoActualizarEvento comando, CancellationToken cancellationToken)
        {
            if (await _repositorioEvento.ListarPorIdEvento(new IdEvento(comando.Id)) is not Evento evento)
            {
                return Error.NotFound("Evento.NoEncontrado", "No se encontro el evento");
            }

            if (comando.HoraInicio >= comando.HoraFin)
            {
                return Error.Validation("HoraInvalida", "La hora de inicio debe ser anterior a la hora de fin.");
            }

            if (!await _repositorioEvento.LugarDisponible(comando.Fecha, comando.HoraInicio, comando.HoraFin, comando.Lugar))
            {
                return Error.Conflict("EventoConflicto", "Ya existe un evento programado en el mismo lugar, fecha y hora.");
            }

            if (await _repositorioEvento.ExisteSolapamiento(comando.Fecha, comando.HoraInicio, comando.HoraFin, comando.Lugar))
            {
                return Error.Conflict("EventoSolapamiento", "El evento se solapa con otro evento ya existente.");
            }

            evento.Actualizar(
                comando.Titulo,
                comando.Descripcion,
                comando.Fecha,
                comando.HoraInicio,
                comando.HoraFin,
                comando.Lugar
            );


            var asistentesActuales = evento.Asistentes.ToList();

            var tareasVerificacion = comando.Asistentes.Select(async asistente =>
            {
                var usuario = await _repositorioUsuario.ListarPorId(new IdUsuario(asistente.Id));
                return (asistente, usuario);
            });

            var resultadosVerificacion = await Task.WhenAll(tareasVerificacion);

            foreach (var (asistente, usuario) in resultadosVerificacion)
            {
                if (usuario is null)
                {
                    return Error.NotFound("Usuario.NoEncontrado", "No se encontró el usuario.");
                }

                if (!asistentesActuales.Any(a => a.Id.Equals(asistente.Id)))
                {
                    evento.AgregarAsistente(new AsistenteDeEvento(
                        new IdAsistenteDeEvento(Guid.NewGuid()),
                        evento.Id,
                        usuario.Id
                    ));
                }
            }

            // Eliminar asistentes que ya no están en la nueva lista
            foreach (var asistenteActual in asistentesActuales)
            {
                if (!comando.Asistentes.Any(a => a.Id.Equals(asistenteActual.Id)))
                {
                    evento.EliminarAsistente(asistenteActual);
                }
            }
            _repositorioEvento.Actualizar(evento);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
