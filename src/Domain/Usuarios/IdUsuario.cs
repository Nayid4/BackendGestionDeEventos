using Domain.Genericos;
using Domain.Primitivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Usuarios
{
    public record IdUsuario(Guid Id) : IIdGenerico;
}
