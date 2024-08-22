using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.EliminarAsistente
{
    public class ValidacionComandoEliminarAsistente : AbstractValidator<ComandoEliminarAsistenteDeEvento>
    {
        public ValidacionComandoEliminarAsistente()
        {
            RuleFor(r => r.IdEvento)
                .NotEmpty();

            RuleFor(r => r.IdUsuario)
                .NotEmpty();
        }
    }
}
