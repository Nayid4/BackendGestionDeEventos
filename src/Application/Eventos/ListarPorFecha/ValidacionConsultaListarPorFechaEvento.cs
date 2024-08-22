using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.ListarPorFecha
{
    public class ValidacionConsultaListarPorFechaEvento : AbstractValidator<ConsultaListarPorFechaEvento>
    {
        public ValidacionConsultaListarPorFechaEvento()
        {
            RuleFor(r => r.Fecha)
                .NotEmpty()
                .Must(fecha => DateTime.TryParse(fecha, out _))
                .WithMessage("La fecha debe tener un formato válido.");
        }
    }
}
