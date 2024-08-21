using Domain.Primitivos;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Actualizar
{
    public sealed class ManejadorComandoActualizarUsuario : IRequestHandler<ComandoActualizarUsuario, ErrorOr<Unit>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IUnitOfWork _unitOfWork;

        public ManejadorComandoActualizarUsuario(IRepositorioUsuario repositorioUsuario, IUnitOfWork unitOfWork)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ComandoActualizarUsuario comando, CancellationToken cancellationToken)
        {
            if (await _repositorioUsuario.ListarPorId(new IdUsuario(comando.Id)) is not Usuario usuario)
            {
                return Error.NotFound("Usuario.NoEncontrado", "Usuario no encontrado.");
            }

            usuario.Actualizar(
                comando.Nombre,
                comando.Apellido,
                comando.Correo
            );

            _repositorioUsuario.Actualizar(usuario);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
