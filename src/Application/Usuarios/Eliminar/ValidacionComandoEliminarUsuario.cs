using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Eliminar
{
    public class ValidacionComandoEliminarUsuario : AbstractValidator<ComandoEliminarUsuario>
    {
        public ValidacionComandoEliminarUsuario()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
        }
    }
}
