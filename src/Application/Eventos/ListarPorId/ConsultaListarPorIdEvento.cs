using Application.Eventos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.ListarPorId
{
    public record ConsultaListarPorIdEvento(Guid Id) : IRequest<ErrorOr<RespuestaEvento>>;
}
