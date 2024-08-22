using Domain.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistencia.Repositorios
{
    public class RepositorioGenerico<T, TId> : IRepositorioGenerico<T, TId>
        where T : IEntidadGenerica<TId>
        where TId : IIdGenerico
    {

        private readonly AplicacionDeContextoDB _contexto;
        protected readonly DbSet<T> _dbSet;

        public RepositorioGenerico(AplicacionDeContextoDB contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _dbSet = _contexto.Set<T>();
        }


        public Task<List<T>> ListarTodos() => _dbSet.ToListAsync();

        public async Task<T?> ListarPorId(TId id) => await _dbSet.FindAsync(id);

        public async void Crear(T entidad) => await _dbSet.AddAsync(entidad);

        public void Actualizar(T entidad) => _dbSet.Update(entidad);

        public void Eliminar(T entidad) => _dbSet.Remove(entidad);

    }
}
