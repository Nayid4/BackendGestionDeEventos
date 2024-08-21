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

            var evento = new Evento(
                new IdEvento(Guid.NewGuid()),
                comando.Titulo,
                comando.Descripcion,
                comando.Fecha,
                comando.HoraInicio,
                comando.HoraFin,
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
