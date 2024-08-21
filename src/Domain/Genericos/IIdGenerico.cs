using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Genericos
{
    public interface IIdGenerico
    {
        Guid Id { get; }
    }
}
