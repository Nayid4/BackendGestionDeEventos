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

            RuleFor(r => r.Categoria)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(r => r.Descripcion)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(r => r.Fecha)
                .NotEmpty()
                .Must(fecha => DateTime.TryParse(fecha, out _))
                .WithMessage("La fecha debe tener un formato válido.");

            RuleFor(r => r.HoraInicio)
                .NotEmpty()
                .Must(hora => TimeSpan.TryParse(hora, out _))
                .WithMessage("La hora de inicio debe tener un formato válido.");

            RuleFor(r => r.HoraFin)
                .NotEmpty()
                .Must(hora => TimeSpan.TryParse(hora, out _))
                .WithMessage("La hora de fin debe tener un formato válido.");


            RuleFor(r => r.Lugar)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
