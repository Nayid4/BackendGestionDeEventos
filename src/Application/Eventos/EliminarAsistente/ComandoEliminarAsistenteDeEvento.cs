using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.EliminarAsistente
{
    public record ComandoEliminarAsistenteDeEvento(Guid IdEvento, Guid IdUsuario): IRequest<ErrorOr<Unit>>;
}
