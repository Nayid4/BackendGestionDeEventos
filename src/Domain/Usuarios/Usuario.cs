using Domain.Genericos;
using Domain.Primitivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Usuarios
{
    public sealed class Usuario : AggregateRoot, IEntidadGenerica<IdUsuario>
    {
        public IdUsuario Id { get; private set; } = default!;
        public string Nombre { get; private set; } = string.Empty;
        public string Apellido { get; private set; } = string.Empty;
        public string Correo { get; private set; } = string.Empty;

        public Usuario()
        {
        }

        public Usuario(IdUsuario idUsuario, string nombre, string apellido, string correo)
        {
            Id = idUsuario ?? throw new ArgumentNullException(nameof(idUsuario));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
        }

        public void Actualizar(string nombre, string apellido, string correo)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
        }
    }
}
