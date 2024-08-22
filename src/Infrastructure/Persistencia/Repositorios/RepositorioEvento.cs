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

        public async Task<bool> LugarDisponible(DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, string lugar, IdEvento? eventoActual)
        {
            return !await _dbSet.AnyAsync(e => e.Fecha == fecha && e.Lugar == lugar
                                               && (horaInicio < e.HoraFin && horaFin > e.HoraInicio)
                                               && (eventoActual == null || !eventoActual.Equals(e.Id)));
        }

        public async Task<bool> ExisteSolapamiento(DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, string lugar, IdEvento? eventoActual)
        {
            return await _dbSet.AnyAsync(e =>
                e.Fecha.Date == fecha.Date && 
                e.Lugar == lugar &&
                (horaInicio < e.HoraInicio && horaFin > e.HoraFin)
                && (eventoActual == null || !eventoActual.Equals(e.Id))
            );
        }

        public async Task<List<Evento>> FiltrarPorCategoria(string categoria)
        {
            return await _dbSet
                .Where(e => e.Categoria == categoria)
                .Include(e => e.Asistentes)
                .ToListAsync();
        }

        public async Task<List<Evento>> FiltrarPorFecha(DateTime fecha)
        {
            return await _dbSet
                .Where(e => e.Fecha.Date == fecha.Date)
                .Include(e => e.Asistentes)
                .ToListAsync();
        }

        public async Task<List<Evento>> FiltrarPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _dbSet
                .Where(e => e.Fecha.Date >= fechaInicio.Date && e.Fecha.Date <= fechaFin.Date)
                .Include(e => e.Asistentes)
                .ToListAsync();
        }

        public async Task<List<Evento>> ObtenerEventosProximos()
        {
            var fechaActual = DateTime.Now.Date;

            return await _dbSet
                .Where(e => e.Fecha.Date >= fechaActual)
                .Include(e => e.Asistentes)
                .ToListAsync();
        }
    }
}
