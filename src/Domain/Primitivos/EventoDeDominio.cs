using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitivos
{
    public record EventoDeDominio(Guid Id): INotification;
}
