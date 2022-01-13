using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuteoPedidos.Core.Model.Motoristas;

namespace RuteoPedidos.Data.Mapping
{
    public class MotoristaMap: IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder.ToTable(nameof(Motorista));

            builder.HasKey(x => x.Codigo);
            builder.HasIndex(x => x.IdUsuario).IsUnique();

            builder.Property(x => x.IdUsuario).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.LatitudUltimaUbicacion).HasColumnType("decimal(9,6)");
            builder.Property(x => x.LongitudUltimaUbicacion).HasColumnType("decimal(9,6)");
            builder.Property(x => x.NombreCompleto).IsRequired().HasMaxLength(200);
            builder.Property(x => x.TelefonoContacto).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PlacasVehiculo).IsRequired().HasMaxLength(100);
            builder.Property(x => x.TextoTipoVehiculo).IsRequired().HasMaxLength(100);

            builder.HasOne(x => x.Cuenta).WithMany().HasForeignKey(f => f.IdCuenta);
            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey(f => f.IdUsuario).IsRequired(false);


        }
    }
}
