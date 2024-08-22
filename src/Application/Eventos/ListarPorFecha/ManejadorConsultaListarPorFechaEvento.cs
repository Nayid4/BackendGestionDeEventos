using Application.Eventos.Comun;
using Domain.Eventos;
using Domain.Primitivos;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Eventos.ListarPorFecha
{
    public sealed class ManejadorConsultaListarPorFechaEvento : IRequestHandler<ConsultaListarPorFechaEvento, ErrorOr<IReadOnlyList<RespuestaEvento>>>
    {
        private readonly IRepositorioEvento _repositorioEvento;
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ManejadorConsultaListarPorFechaEvento(IRepositorioEvento repositorioEvento, IRepositorioUsuario repositorioUsuario)
        {
            _repositorioEvento = repositorioEvento ?? throw new ArgumentNullException(nameof(repositorioEvento));
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }
        public async Task<ErrorOr<IReadOnlyList<RespuestaEvento>>> Handle(ConsultaListarPorFechaEvento consulta, CancellationToken cancellationToken)
        {
            if (!DateTime.TryParse(consulta.Fecha, out var fecha))
            {
                return Error.Validation("FechaInvalida", "La fecha no tiene un formato válido.");
            }

            var listaDeEventos = await _repositorioEvento.FiltrarPorFecha(fecha);

            var listaDeRespuestas = new List<RespuestaEvento>();

            foreach (var evento in listaDeEventos)
            {
                var asistentesRespuesta = new List<RepuestaAsistente>();

                foreach (var asistente in evento.Asistentes)
                {
                    var usuario = await _repositorioUsuario.ListarPorId(asistente.IdUsuario);

                    if (usuario is null)
                    {
                        return Error.NotFound("Usuario.NoEncontrado", "No se encontró el usuario.");
                    }

                    var respuestaAsistente = new RepuestaAsistente(
                        usuario.Id.Id,
                        usuario.Nombre,
                        usuario.Apellido,
                        usuario.Correo
                    );

                    asistentesRespuesta.Add(respuestaAsistente);
                }

                var respuestaEvento = new RespuestaEvento(
                    evento.Id.Id,
                    evento.Titulo,
                    evento.Categoria,
                    evento.Descripcion,
                    evento.Fecha,
                    evento.HoraInicio,
                    evento.HoraFin,
                    evento.Lugar,
                    asistentesRespuesta,
                    evento.FechaCreacion,
                    evento.FechaActualizacion
                );

                listaDeRespuestas.Add(respuestaEvento);
            }

            return listaDeRespuestas.AsReadOnly();
        }
    }
}
