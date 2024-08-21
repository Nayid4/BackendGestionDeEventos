using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Crear
{
    public class ValidacionComandoCrearEvento : AbstractValidator<ComandoCrearEvento>
    {
        public ValidacionComandoCrearEvento()
        {
            RuleFor(r => r.Titulo)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(r => r.Descripcion)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(r => r.Fecha)
                .NotEmpty();

            RuleFor(r => r.Hora)
                .NotEmpty();

            RuleFor(r => r.Lugar)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
