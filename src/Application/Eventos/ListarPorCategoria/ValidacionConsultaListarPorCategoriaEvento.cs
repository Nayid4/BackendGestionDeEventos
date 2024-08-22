using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.ListarPorCategoria
{
    public class ValidacionConsultaListarPorCategoriaEvento : AbstractValidator<ConsultaListarPorCategoriaEvento>
    {
        public ValidacionConsultaListarPorCategoriaEvento()
        {
            RuleFor(r => r.Categoria)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
