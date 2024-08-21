using Application.Usuarios.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.ListarPorId
{
    public record ConsultaListarPorIdUsuario(Guid Id): IRequest<ErrorOr<RespuestaUsuario>>;
}
