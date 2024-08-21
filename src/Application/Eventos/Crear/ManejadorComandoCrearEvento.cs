using Domain.Eventos;
using Domain.Primitivos;
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
        private readonly IUnitOfWork _unitOfWork;

        public ManejadorComandoCrearEvento(IRepositorioEvento repositorioEvento, IUnitOfWork unitOfWork)
        {
            _repositorioEvento = repositorioEvento ?? throw new ArgumentNullException(nameof(repositorioEvento));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task<ErrorOr<Unit>> Handle(ComandoCrearEvento comando, CancellationToken cancellationToken)
        {
            
        }
    }
}
