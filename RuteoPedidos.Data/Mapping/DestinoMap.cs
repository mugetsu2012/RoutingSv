using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Data.Mapping
{
    public class DestinoMap: IEntityTypeConfiguration<Destino>
    {
        public void Configure(EntityTypeBuilder<Destino> builder)
        {
            builder.ToTable(nameof(Destino));

            builder.HasKey(x => x.Codigo);

            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.LatitudUbicacion).HasColumnType("decimal(9,6)");
            builder.Property(x => x.LongitudUbicacion).HasColumnType("decimal(9,6)");
            builder.Property(x => x.Direccion).IsRequired(false);
            builder.Property(x => x.TelefonoContacto).IsRequired(false).HasMaxLength(100);

            builder.HasOne(x => x.Cuenta).WithMany().HasForeignKey(f => f.IdCuenta);
        }
    }
}
