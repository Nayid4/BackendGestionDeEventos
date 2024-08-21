using Application.Eventos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Crear
{
    public record ComandoCrearEvento(
        string Titulo,
        string Descripcion,
        DateOnly Fecha,
        TimeOnly HoraInicio,
        TimeOnly HoraFin,
        string Lugar,
        List<ComandoAsistente> Asistentes
    ): IRequest<ErrorOr<Unit>>;
}
