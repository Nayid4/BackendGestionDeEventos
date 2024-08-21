using Domain.Usuarios;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistencia.Configuraciones
{
    public class ConfiguracionUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasConversion(
                usuarioId => usuarioId.Id,
                valor => new IdUsuario(valor))
                .IsRequired();

            builder.Property(u => u.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Apellido)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Correo)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
