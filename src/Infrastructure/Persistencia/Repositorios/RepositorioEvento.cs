using Domain.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistencia.Repositorios
{ 

    public class RepositorioEvento : RepositorioGenerico<Evento, IdEvento>, IRepositorioEvento
    {
        public RepositorioEvento(AplicacionDeContextoDB contexto) : base(contexto)
        {
        }

        public async Task<Evento?> ListarPorIdEvento(IdEvento id)
        {
            return await _dbSet.Include(e => e.Asistentes).FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public Task<List<Evento>> ListarTodosLosEventos()
        {
            return _dbSet.Include(e => e.Asistentes).ToListAsync();
        }
    }
}
