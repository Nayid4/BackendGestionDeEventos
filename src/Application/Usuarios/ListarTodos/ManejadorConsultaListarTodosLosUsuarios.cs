using Application.Usuarios.Comun;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.ListarTodos
{
    public sealed class ManejadorConsultaListarTodosLosUsuarios : IRequestHandler<ConsultaListarTodosLosUsuarios, ErrorOr<IReadOnlyList<RespuestaUsuario>>>
    {
        public readonly IRepositorioUsuario _repositorioUsuario;

        public ManejadorConsultaListarTodosLosUsuarios(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<ErrorOr<IReadOnlyList<RespuestaUsuario>>> Handle(ConsultaListarTodosLosUsuarios consulta, CancellationToken cancellationToken)
        {
            IReadOnlyList<Usuario> listaUsuarios = await _repositorioUsuario.ListarTodos();

            var lista = listaUsuarios.Select(u => new RespuestaUsuario(
                    u.Id.Id,
                    u.Nombre,
                    u.Apellido,
                    u.Correo
                )).ToList();

            return lista;
        }
    }
}
