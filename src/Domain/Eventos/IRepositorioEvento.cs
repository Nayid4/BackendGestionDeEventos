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
        Task<bool> LugarDisponible(DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, string lugar, IdEvento? eventoActual = null);
        Task<bool> ExisteSolapamiento(DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, string lugar, IdEvento? eventoActual = null);
        Task<List<Evento>> FiltrarPorCategoria(string categoria);

        Task<List<Evento>> FiltrarPorFecha(DateTime fecha);

        Task<List<Evento>> FiltrarPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFin);

        Task<List<Evento>> ObtenerEventosProximos();
    }
}
