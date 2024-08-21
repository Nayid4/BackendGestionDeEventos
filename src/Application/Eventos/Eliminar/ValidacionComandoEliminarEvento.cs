using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Eliminar
{
    public class ValidacionComandoEliminarEvento : AbstractValidator<ComandoEliminarEvento>
    {
        public ValidacionComandoEliminarEvento()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
        }
    }
}
