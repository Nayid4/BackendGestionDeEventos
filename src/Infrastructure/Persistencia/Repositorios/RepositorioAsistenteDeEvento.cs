using Domain.AsistentesDeEventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistencia.Repositorios
{
    public class RepositorioAsistenteDeEvento : RepositorioGenerico<AsistenteDeEvento, IdAsistenteDeEvento>, IRepositorioAsistenteDeEvento
    {
        public RepositorioAsistenteDeEvento(AplicacionDeContextoDB contexto) : base(contexto)
        {
        }
    }
}
