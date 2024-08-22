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

namespace Application.Eventos.AgregarAsistente
{
    public sealed class ManejadorComandoAgregarAsistenteDeEvento : IRequestHandler<ComandoAgregarAsistenteDeEvento, ErrorOr<Unit>>
    {
        private readonly IRepositorioEvento _repositorioEvento;
        private readonly IUnitOfWork _unitOfWork;

        public ManejadorComandoAgregarAsistenteDeEvento(IRepositorioEvento repositorioEvento, IUnitOfWork unitOfWork)
        {
            _repositorioEvento = repositorioEvento ?? throw new ArgumentNullException(nameof(repositorioEvento));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ComandoAgregarAsistenteDeEvento comando, CancellationToken cancellationToken)
        {
            if (await _repositorioEvento.ListarPorIdEvento(new IdEvento(comando.IdEvento)) is not Evento evento)
            {
                return Error.NotFound("Evento.NoEncontrado", "No se encontro el evento");
            }

            if (evento.ObtenerAsistentePorIdUsuario(new IdUsuario(comando.IdUsuario)) is AsistenteDeEvento asistente)
            {
                return Error.Validation("Evento.AsistenteEncontrado","El usuario ya es asistente del evento.");
            }

            var asistenteNuevo = new AsistenteDeEvento(
                new IdAsistenteDeEvento(Guid.NewGuid()),
                evento.Id,
                new IdUsuario(comando.IdUsuario)
            );

            evento.AgregarAsistente(asistenteNuevo);

            _repositorioEvento.Actualizar(evento);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
