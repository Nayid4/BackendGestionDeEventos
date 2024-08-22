using Domain.Eventos;
using Domain.Primitivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Eliminar
{
    public class ManejadorComandoEliminarEvento : IRequestHandler<ComandoEliminarEvento, ErrorOr<Unit>>
    {
        private readonly IRepositorioEvento _repositorioEvento;
        private readonly IUnitOfWork _unitOfWork;

        public ManejadorComandoEliminarEvento(IRepositorioEvento repositorioEvento, IUnitOfWork unitOfWork)
        {
            _repositorioEvento = repositorioEvento ?? throw new ArgumentNullException(nameof(repositorioEvento));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ComandoEliminarEvento comando, CancellationToken cancellationToken)
        {
            if (await _repositorioEvento.ListarPorIdEvento(new IdEvento(comando.Id)) is not Evento evento)
            {
                return Error.NotFound("Evento.NoEncontrado", "No se encontro el evento");
            }

            _repositorioEvento.Eliminar(evento);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
