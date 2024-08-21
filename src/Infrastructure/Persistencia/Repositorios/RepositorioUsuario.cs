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

        public async Task<Usuario?> ListarPorCorreo(string correo)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Correo.Equals(correo));
        }
    }
}
