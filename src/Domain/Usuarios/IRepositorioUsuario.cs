using Domain.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Usuarios
{
    public interface IRepositorioUsuario : IRepositorioGenerico<Usuario, IdUsuario>
    {
    }
}
