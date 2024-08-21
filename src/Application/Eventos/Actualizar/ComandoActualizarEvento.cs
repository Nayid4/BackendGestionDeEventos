using Application.Eventos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Actualizar
{
    public record ComandoActualizarEvento(
        Guid Id,
        string Titulo,
        string Descripcion,
        DateOnly Fecha,
        TimeOnly HoraInicio,
        TimeOnly HoraFin,
        string Lugar,
        List<ComandoAsistente> Asistentes
    ) : IRequest<ErrorOr<Unit>>;
}
