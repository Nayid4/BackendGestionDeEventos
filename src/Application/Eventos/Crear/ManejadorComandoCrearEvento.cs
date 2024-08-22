using Domain.AsistentesDeEventos;
using Domain.Eventos;
using Domain.Primitivos;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Crear
{
    public sealed class ManejadorComandoCrearEvento : IRequestHandler<ComandoCrearEvento, ErrorOr<Unit>>
    {
        private readonly IRepositorioEvento _repositorioEvento;
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IUnitOfWork _unitOfWork;

        public ManejadorComandoCrearEvento(IRepositorioEvento repositorioEvento, IRepositorioUsuario repositorioUsuario, IUnitOfWork unitOfWork)
        {
            _repositorioEvento = repositorioEvento ?? throw new ArgumentNullException(nameof(repositorioEvento));
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ComandoCrearEvento comando, CancellationToken cancellationToken)
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

            if (horaInicio >= horaFin)
            {
                return Error.Validation("HoraInvalida", "La hora de inicio debe ser anterior a la hora de fin.");
            }

            if (!await _repositorioEvento.LugarDisponible(fecha, horaInicio, horaFin, comando.Lugar))
            {
                return Error.Conflict("EventoConflicto", "Ya existe un evento programado en el mismo lugar, fecha y hora.");
            }

            if (await _repositorioEvento.ExisteSolapamiento(fecha, horaInicio,horaFin, comando.Lugar))
            {
                return Error.Conflict("EventoSolapamiento", "El evento se solapa con otro evento ya existente.");
            }

            var evento = new Evento(
                new IdEvento(Guid.NewGuid()),
                comando.Titulo,
                comando.Categoria,
                comando.Descripcion,
                fecha,
                horaInicio,
                horaFin,
                comando.Lugar
            );


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

                evento.AgregarAsistente(new AsistenteDeEvento(
                    new IdAsistenteDeEvento(Guid.NewGuid()),
                    evento.Id,
                    usuario.Id
                ));
            }

            _repositorioEvento.Crear(evento);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
