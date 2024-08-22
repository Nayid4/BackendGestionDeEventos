using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.ListarPorRangoDeFecha
{
    public class ValidacionConsultaListarPorRangoDeFechaEvento : AbstractValidator<ConsultaListarPorRangoDeFechaEvento>
    {
        public ValidacionConsultaListarPorRangoDeFechaEvento()
        {
            RuleFor(r => r.FechaInicio)
                .NotEmpty()
                .Must(fecha => DateTime.TryParse(fecha, out _))
                .WithMessage("La fecha debe tener un formato válido.");

            RuleFor(r => r.FechaFin)
                .NotEmpty()
                .Must(fecha => DateTime.TryParse(fecha, out _))
                .WithMessage("La fecha debe tener un formato válido.");
        }
    }
}
