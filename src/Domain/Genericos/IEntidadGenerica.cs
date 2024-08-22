using Domain.Primitivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Genericos
{
    public abstract class IEntidadGenerica<TId> : AggregateRoot
        where TId : IIdGenerico
    {
        public TId Id { get; protected set; } = default!;
        public DateTime FechaCreacion { get; protected set; }
        public DateTime FechaActualizacion { get; protected set; }

        protected IEntidadGenerica()
        {
            FechaCreacion = DateTime.Now;
            FechaActualizacion = DateTime.Now;
        }

        protected IEntidadGenerica(TId id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            FechaCreacion = DateTime.Now;
            FechaActualizacion = DateTime.Now;
        }
    }
}
