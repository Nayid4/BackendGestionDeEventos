using Application.Usuarios.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.ListarTodos
{
    public record ConsultaListarTodosLosUsuarios() : IRequest<ErrorOr<IReadOnlyList<RespuestaUsuario>>>;
}
