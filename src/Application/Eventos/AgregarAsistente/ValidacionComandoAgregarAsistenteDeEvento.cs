using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.AgregarAsistente
{
    public class ValidacionComandoAgregarAsistenteDeEvento : AbstractValidator<ComandoAgregarAsistenteDeEvento>
    {
        public ValidacionComandoAgregarAsistenteDeEvento()
        {
            RuleFor(r => r.IdEvento)
                .NotEmpty();

            RuleFor(r => r.IdUsuario)
                .NotEmpty();
        }
    }
}
