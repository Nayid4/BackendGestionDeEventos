using Application.Eventos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.ListarPorFecha
{
    public record ConsultaListarPorFechaEvento(string Fecha) : IRequest<ErrorOr<IReadOnlyList<RespuestaEvento>>>;
}
