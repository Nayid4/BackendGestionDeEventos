using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistencia.Repositorios
{
    public class RepositorioUsuario : RepositorioGenerico<Usuario, IdUsuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(AplicacionDeContextoDB contexto) : base(contexto)
        {
        }
    }
}
