using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Comun
{
    public record RespuestaEvento(
        Guid Id,
        string Titulo,
        string Categoria,
        string Descripcion,
        DateTime Fecha,
        TimeSpan HoraInicio,
        TimeSpan HoraFin,
        string Lugar,
        List<RepuestaAsistente> Asistentes,
        DateTime FechaCreacion,
        DateTime FechaActualizacion
    );

    public record RepuestaAsistente(
        Guid Id,
        string Nombre,
        string Apellido,
        string Correo
    );
}
