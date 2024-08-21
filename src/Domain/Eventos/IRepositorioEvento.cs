using Domain.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Eventos
{
    public interface IRepositorioEvento : IRepositorioGenerico<Evento, IdEvento>
    {
        Task<List<Evento>> ListarTodosLosEventos();
        Task<Evento?> ListarPorIdEvento(IdEvento id);
    }
}
