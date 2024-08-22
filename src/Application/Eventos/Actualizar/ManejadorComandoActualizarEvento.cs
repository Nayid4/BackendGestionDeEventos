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
            if (!DateTime.TryParse(comando.Fecha, out var fecha))
            {
                return Error.Validation("FechaInvalida", "La fecha no tiene un formato válido.");
            }

            // Conversión de Horas
            if (!TimeSpan.TryParse(comando.HoraInicio, out var horaInicio))
            {
                return Error.Validation("HoraInvalida", "La hora de inicio no tiene un formato válido.");
            }

            if (!TimeSpan.TryParse(comando.HoraFin, out var horaFin))
            {
                return Error.Validation("HoraInvalida", "La hora de fin no tiene un formato válido.");
            }

            if (await _repositorioEvento.ListarPorIdEvento(new IdEvento(comando.Id)) is not Evento evento)
            {
                return Error.NotFound("Evento.NoEncontrado", "No se encontro el evento");
            }

            if (horaInicio >= horaFin)
            {
                return Error.Validation("HoraInvalida", "La hora de inicio debe ser anterior a la hora de fin.");
            }

            if (!await _repositorioEvento.LugarDisponible(fecha, horaInicio, horaFin, comando.Lugar, evento.Id))
            {
                return Error.Conflict("EventoConflicto", "Ya existe un evento programado en el mismo lugar, fecha y hora.");
            }

            if (await _repositorioEvento.ExisteSolapamiento(fecha, horaInicio, horaFin, comando.Lugar, evento.Id))
            {
                return Error.Conflict("EventoSolapamiento", "El evento se solapa con otro evento ya existente.");
            }

            evento.Actualizar(
                comando.Titulo,
                comando.Categoria,
                comando.Descripcion,
                fecha,
                horaInicio,
                horaFin,
                comando.Lugar
            );


            var asistentesActuales = evento.Asistentes.ToList();

            if (comando.Asistentes.Count == 0)
            {
                // Eliminar todos los asistentes si la lista de asistentes está vacía
                foreach (var asistenteActual in asistentesActuales)
                {
                    evento.EliminarAsistente(asistenteActual);
                }
            }
            else
            {
                // Agregar y eliminar asistentes según corresponda
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
                foreach (var asistenteActual in asistentesActuales.ToList())
                {
                    if (!comando.Asistentes.Any(a => a.Id.Equals(asistenteActual.Id)))
                    {
                        evento.EliminarAsistente(asistenteActual);
                    }
                }
            }


            _repositorioEvento.Actualizar(evento);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
