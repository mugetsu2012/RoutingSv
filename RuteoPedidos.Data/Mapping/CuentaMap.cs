using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuteoPedidos.Core.Model;

namespace RuteoPedidos.Data.Mapping
{
    public class CuentaMap: IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable(nameof(Cuenta));

            builder.HasKey(x => x.Codigo);

            builder.Property(x => x.NombreCuenta).IsRequired().HasMaxLength(200);
            builder.Property(x => x.NombreContacto).IsRequired().HasMaxLength(200);
            builder.Property(x => x.TelefonoContacto).IsRequired().HasMaxLength(200);
        }
    }
}
