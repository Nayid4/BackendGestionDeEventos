using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Actualizar
{
    public class ValidacionComandoActualizarEvento : AbstractValidator<ComandoActualizarEvento>
    {
        public ValidacionComandoActualizarEvento()
        {
            RuleFor(r => r.Id)
                .NotEmpty();

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
