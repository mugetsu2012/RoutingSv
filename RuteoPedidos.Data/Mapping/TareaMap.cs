using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Data.Mapping
{
    public class TareaMap: IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            builder.ToTable(nameof(Tarea));

            builder.HasKey(x => x.Codigo);

            builder.Property(x => x.DestinoCliente).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.LatitudUbicacion).HasColumnType("decimal(9,6)");
            builder.Property(x => x.LongitudUbicacion).HasColumnType("decimal(9,6)");
            builder.Property(x => x.Indicaciones).IsRequired(false);
            builder.Property(x => x.TelefonoContacto).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.DetalleTarea).IsRequired();
            builder.Property(x => x.TextoEstadoTarea).IsRequired().HasMaxLength(100);

            builder.HasOne(x => x.Destino).WithMany().HasForeignKey(f => f.IdClienteUtilizado).IsRequired(false);
            builder.HasOne(x => x.Cuenta).WithMany().HasForeignKey(f => f.IdCuenta);
            builder.HasOne(x => x.Motorista).WithMany().HasForeignKey(f => f.IdMotoristaAsignado).IsRequired(false);
        }
    }
}
