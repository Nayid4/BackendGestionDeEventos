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
        string Categoria,
        string Descripcion,
        string Fecha,
        string HoraInicio,
        string HoraFin,
        string Lugar,
        List<ComandoAsistente> Asistentes
    ) : IRequest<ErrorOr<Unit>>;
}
