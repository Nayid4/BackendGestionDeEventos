using Domain.Eventos;
using Domain.Genericos;
using Domain.Primitivos;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AsistentesDeEventos
{
    public sealed class AsistenteDeEvento : IEntidadGenerica<IdAsistenteDeEvento>
    {
        public IdEvento IdEvento { get; private set; } = default!;
        public IdUsuario IdUsuario { get; private set; } = default!;

        public AsistenteDeEvento() : base()
        {
        }

        public AsistenteDeEvento(IdAsistenteDeEvento id, IdEvento idEvento, IdUsuario idUsuario) : base(id)
        {
            IdEvento = idEvento ?? throw new ArgumentNullException(nameof(idEvento));
            IdUsuario = idUsuario ?? throw new ArgumentNullException(nameof(idUsuario));
        }

        public void Actualizar(IdEvento idEvento, IdUsuario idUsuario)
        {
            IdEvento = idEvento ?? throw new ArgumentNullException(nameof(idEvento));
            IdUsuario = idUsuario ?? throw new ArgumentNullException(nameof(idUsuario));
            FechaActualizacion = DateTime.Now;
        }
    }
}
