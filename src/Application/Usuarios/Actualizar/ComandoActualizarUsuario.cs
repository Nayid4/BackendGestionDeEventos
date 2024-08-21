using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Actualizar
{
    public record ComandoActualizarUsuario(
        Guid Id,
        string Nombre,
        string Apellido,
        string Correo
    ) : IRequest<ErrorOr<Unit>>;
}
