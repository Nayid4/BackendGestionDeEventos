using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitivos
{
    public abstract class AggregateRoot
    {
        private readonly List<EventoDeDominio> _eventosDeDominio = new();

        public ICollection<EventoDeDominio> GetDomainEvents() => _eventosDeDominio;

        protected void Raise(EventoDeDominio eventoDeDominio)
        {
            _eventosDeDominio.Add(eventoDeDominio);
        }
    }
}
