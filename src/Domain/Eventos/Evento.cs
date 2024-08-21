using Domain.AsistentesDeEventos;
using Domain.Genericos;
using Domain.Primitivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Eventos
{
    public sealed class Evento : IEntidadGenerica<IdEvento>
    {
        public string Titulo { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;
        public DateOnly Fecha { get; private set; }
        public TimeOnly Hora { get; private set; }
        public string Lugar { get; private set; } = string.Empty;

        private readonly HashSet<AsistenteDeEvento> _asistentes = new();

        public ICollection<AsistenteDeEvento> Asistentes => _asistentes.ToList();

        public Evento() : base()
        {
        }

        public Evento(IdEvento id, string titulo, string descripcion, DateOnly fecha, TimeOnly hora, string lugar) : base(id)
        {
            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            Fecha = fecha;
            Hora = hora;
            Lugar = lugar ?? throw new ArgumentNullException(nameof(lugar));
        }

        public void Actualizar(string titulo, string descripcion, DateOnly fecha, TimeOnly hora, string lugar)
        {
            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            Fecha = fecha;
            Hora = hora;
            Lugar = lugar ?? throw new ArgumentNullException(nameof(lugar));
            FechaActualizacion = DateTime.Now;
        }
    }
}
