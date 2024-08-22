using Application.Eventos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.ListarPorRangoDeFecha
{
    public record ConsultaListarPorRangoDeFechaEvento(
        string FechaInicio, 
        string FechaFin
    ) : IRequest<ErrorOr<IReadOnlyList<RespuestaEvento>>>;
}
