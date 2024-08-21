using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Crear
{
    public class ValidacionComandoCrearUsuario : AbstractValidator<ComandoCrearUsuario>
    {
        public ValidacionComandoCrearUsuario()
        {
            RuleFor(r => r.Nombre)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(r => r.Apellido)
                .MaximumLength(50);

            RuleFor(r => r.Correo)
                .MaximumLength(255)
                .NotEmpty();
        }
    }
}
