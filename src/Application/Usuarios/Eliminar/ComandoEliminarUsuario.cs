using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Eliminar
{
    public record ComandoEliminarUsuario(Guid Id): IRequest<ErrorOr<Unit>>;
}
