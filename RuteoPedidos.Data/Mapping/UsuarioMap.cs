using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuteoPedidos.Core.Model;

namespace RuteoPedidos.Data.Mapping
{
    public class UsuarioMap: IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario));

            builder.HasKey(x => x.IdUsuario);
            builder.HasIndex(x => new {x.Email, x.IdCuenta}).IsUnique();

            builder.Property(x => x.IdUsuario).HasMaxLength(100);
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Apellido).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);

            builder.HasOne(x => x.Cuenta).WithMany().HasForeignKey(f => f.IdCuenta);

        }
    }
}
