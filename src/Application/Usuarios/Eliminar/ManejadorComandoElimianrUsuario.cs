using Domain.Primitivos;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Eliminar
{
    public sealed class ManejadorComandoElimianrUsuario : IRequestHandler<ComandoEliminarUsuario, ErrorOr<Unit>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IUnitOfWork _unitOfWork;

        public ManejadorComandoElimianrUsuario(IRepositorioUsuario repositorioUsuario, IUnitOfWork unitOfWork)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ComandoEliminarUsuario comando, CancellationToken cancellationToken)
        {
            if (await _repositorioUsuario.ListarPorId(new IdUsuario(comando.Id)) is not Usuario usuario)
            {
                return Error.NotFound("Usuario.NoEncontrado","Usuario no encontrado.");
            }

            _repositorioUsuario.Eliminar(usuario);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
