using Domain.AsistentesDeEventos;
using Domain.Eventos;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistencia.Configuraciones
{
    public class ConfiguracionAsistenteDeEvento : IEntityTypeConfiguration<AsistenteDeEvento>
    {
        public void Configure(EntityTypeBuilder<AsistenteDeEvento> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).HasConversion(
                Idasistente => Idasistente.Id,
                valor => new IdAsistenteDeEvento(valor))
                .IsRequired();

            builder.Property(a => a.IdEvento).HasConversion(
                idEvento => idEvento.Id,
                valor => new IdEvento(valor))
                .IsRequired();

            builder.Property(a => a.IdUsuario).HasConversion(
                idUsuario => idUsuario.Id,
                valor => new IdUsuario(valor))
                .IsRequired();

            builder.Property(a => a.FechaCreacion)
                .IsRequired();

            builder.Property(a => a.FechaActualizacion)
                .IsRequired();
        }
    }
}
