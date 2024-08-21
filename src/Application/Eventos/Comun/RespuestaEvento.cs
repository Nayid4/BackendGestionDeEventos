using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Comun
{
    public record RespuestaEvento(
        string Titulo,
        string Descripcion,
        DateOnly Fecha,
        TimeOnly Hora,
        string Lugar,
        List<RepuestaAsistente> Asistentes
    );

    public record RepuestaAsistente(
        Guid Id,
        string Nombre,
        string Apellido,
        string Correo
    );
}
