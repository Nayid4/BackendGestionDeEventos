using Application.Usuarios.Comun;
using Domain.Primitivos;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Usuarios.ListarPorId
{
    public class ManejadorConsultaListarPorIdUsuario : IRequestHandler<ConsultaListarPorIdUsuario, ErrorOr<RespuestaUsuario>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ManejadorConsultaListarPorIdUsuario(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<ErrorOr<RespuestaUsuario>> Handle(ConsultaListarPorIdUsuario consulta, CancellationToken cancellationToken)
        {
            if (await _repositorioUsuario.ListarPorId(new IdUsuario(consulta.Id)) is not Usuario usuario)
            {
                return Error.NotFound("Usuario.NoEncontrado", "Usuario no encontrado.");
            }

            var respuestaUsuario = new RespuestaUsuario(
                usuario.Id.Id,
                usuario.Nombre,
                usuario.Apellido,
                usuario.Correo
            );

            return respuestaUsuario;
        }
    }
}
