using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Genericos
{
    public interface IRepositorioGenerico<T, TId>
        where T : class, IEntidadGenerica<TId>
        where TId : IIdGenerico
    {
        Task<List<T>> ListarTodos();
        Task<T?> ListarPorId(TId id);
        void Crear(T entidad);
        void Actualizar(T entidad);
        void Eliminar(T entidad);
    }
}
