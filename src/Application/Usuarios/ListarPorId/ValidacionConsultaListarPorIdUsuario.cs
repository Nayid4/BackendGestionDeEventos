using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.ListarPorId
{
    public class ValidacionConsultaListarPorIdUsuario : AbstractValidator<ConsultaListarPorIdUsuario>
    {
        public ValidacionConsultaListarPorIdUsuario()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
        }
    }
}
