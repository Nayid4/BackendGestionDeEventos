using Domain.AsistentesDeEventos;
using Domain.Genericos;
using Domain.Primitivos;
using Domain.Usuarios;
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
        public string Categoria { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;
        public DateTime Fecha { get; private set; }
        public TimeSpan HoraInicio { get; private set; }
        public TimeSpan HoraFin { get; private set; }
        public string Lugar { get; private set; } = string.Empty;

        private readonly HashSet<AsistenteDeEvento> _asistentes = new();

        public ICollection<AsistenteDeEvento> Asistentes => _asistentes.ToList();

        public Evento() : base()
        {
        }

        public Evento(IdEvento id, string titulo, string categoria, string descripcion, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, string lugar) : base(id)
        {
            if (horaInicio >= horaFin)
            {
                throw new ArgumentException("La hora de inicio debe ser anterior a la hora de fin.");
            }

            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            Categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            Fecha = fecha;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Lugar = lugar ?? throw new ArgumentNullException(nameof(lugar));
        }

        public void Actualizar(string titulo, string categoria,string descripcion, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, string lugar)
        {
            if (horaInicio >= horaFin)
            {
                throw new ArgumentException("La hora de inicio debe ser anterior a la hora de fin.");
            }

            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            Categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            Fecha = fecha;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Lugar = lugar ?? throw new ArgumentNullException(nameof(lugar));
            FechaActualizacion = DateTime.Now;
        }


        public void AgregarAsistente(AsistenteDeEvento asistente)
        {
            if (_asistentes.Contains(asistente))
            {
                throw new InvalidOperationException("El asistente ya está registrado en el evento.");
            }

            _asistentes.Add(asistente ?? throw new ArgumentNullException(nameof(asistente)));
        }

        public void EliminarAsistente(AsistenteDeEvento asistente)
        {
            if (!_asistentes.Contains(asistente))
            {
                throw new InvalidOperationException("El asistente no está registrado en el evento.");
            }

            _asistentes.Remove(asistente ?? throw new ArgumentNullException(nameof(asistente)));
        }

        public AsistenteDeEvento? ObtenerAsistentePorIdUsuario(IdUsuario idUsuario)
        {
            if (idUsuario == null)
            {
                throw new ArgumentNullException(nameof(idUsuario));
            }

            return _asistentes.FirstOrDefault(asistente => asistente.IdUsuario.Equals(idUsuario));
        }

        public void EliminarTodosLosAsistente()
        {

            _asistentes.Clear();
        }

    }
}
