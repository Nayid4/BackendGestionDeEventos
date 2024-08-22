using Domain.Primitivos;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Crear
{
    public sealed class ManejadorComandoCrearUsuario : IRequestHandler<ComandoCrearUsuario, ErrorOr<Unit>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IUnitOfWork _unitOfWork;

        public ManejadorComandoCrearUsuario(IRepositorioUsuario repositorioUsuario, IUnitOfWork unitOfWork)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ComandoCrearUsuario comando, CancellationToken cancellationToken)
        {
            if (await _repositorioUsuario.ListarPorCorreo(comando.Correo) is Usuario usuario)
            {
                return Error.Validation("Usuario.Encontrado", "Ya existe un usuario con ese correo.");
            }

            Usuario usuarioRegistro = new Usuario(
                new IdUsuario(Guid.NewGuid()),
                comando.Nombre,
                comando.Apellido,
                comando.Correo
            );

            _repositorioUsuario.Crear(usuarioRegistro);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
