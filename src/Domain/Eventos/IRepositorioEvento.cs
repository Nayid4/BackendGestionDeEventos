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
        Task<Evento?> ListarPorDatos(DateOnly fecha, TimeOnly HoraInicio, TimeOnly HoraFin, string Lugar);
        Task<bool> LugarDisponible(DateOnly fecha, TimeOnly horaInicio, TimeOnly horaFin, string lugar);
        Task<bool> ExisteSolapamiento(DateOnly fecha, TimeOnly horaInicio, TimeOnly horaFin, string lugar);
    }
}
