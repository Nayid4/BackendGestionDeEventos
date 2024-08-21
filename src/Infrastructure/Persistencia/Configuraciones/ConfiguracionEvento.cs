using Domain.Eventos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistencia.Configuraciones
{
    public class ConfiguracionEvento : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(
                idEvento => idEvento.Id,
                valor => new IdEvento(valor))
                .IsRequired();

            builder.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Fecha)
                .IsRequired();

            builder.Property(e => e.Hora)
                .IsRequired();

            builder.Property(e => e.Lugar)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(e => e.Asistentes)
                .WithOne()
                .HasForeignKey(a => a.IdEvento);
        }
    }
}
