using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Comun
{
    public record RespuestaUsuario(
        Guid Id,
        string Nombre,
        string Apellido,
        string Correo
    );
}
