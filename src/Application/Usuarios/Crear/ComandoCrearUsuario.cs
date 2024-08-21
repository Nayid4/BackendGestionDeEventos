using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Crear
{
    public record ComandoCrearUsuario(
        string Nombre,
        string Apellido,
        string Correo
    ): IRequest<ErrorOr<Unit>>;
}
