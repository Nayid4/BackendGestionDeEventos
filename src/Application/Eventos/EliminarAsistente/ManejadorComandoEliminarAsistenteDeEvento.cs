using Domain.AsistentesDeEventos;
using Domain.Eventos;
using Domain.Primitivos;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.EliminarAsistente
{
    public sealed class ManejadorComandoEliminarAsistenteDeEvento : IRequestHandler<ComandoEliminarAsistenteDeEvento, ErrorOr<Unit>>
    {
        private readonly IRepositorioEvento _repositorioEvento;
        private readonly IUnitOfWork _unitOfWork;

        public ManejadorComandoEliminarAsistenteDeEvento(IRepositorioEvento repositorioEvento, IUnitOfWork unitOfWork)
        {
            _repositorioEvento = repositorioEvento ?? throw new ArgumentNullException(nameof(repositorioEvento));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ComandoEliminarAsistenteDeEvento comando, CancellationToken cancellationToken)
        {
            if (await _repositorioEvento.ListarPorIdEvento(new IdEvento(comando.IdEvento)) is not Evento evento)
            {
                return Error.NotFound("Evento.NoEncontrado", "No se encontro el evento");
            }

            if (evento.ObtenerAsistentePorIdUsuario(new IdUsuario(comando.IdUsuario)) is not AsistenteDeEvento asistente)
            {
                return Error.Validation("Evento.AsistenteNoEncontrado", "El usuario no es asistente del evento.");
            }


            evento.AgregarAsistente(asistente);

            return Unit.Value;
        }
    }
}
