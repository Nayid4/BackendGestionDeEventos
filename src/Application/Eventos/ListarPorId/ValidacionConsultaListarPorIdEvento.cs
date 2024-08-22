using Application.Eventos.Comun;
using Domain.Eventos;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Eventos.ListarPorId
{
    public class ValidacionConsultaListarPorIdEvento : AbstractValidator<ConsultaListarPorIdEvento>
    {
        public ValidacionConsultaListarPorIdEvento()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
        }
    }

}
