using Application.Eventos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.ListarPorCategoria
{
    public record ConsultaListarPorCategoriaEvento(string Categoria) : IRequest<ErrorOr<IReadOnlyList<RespuestaEvento>>>;
}
