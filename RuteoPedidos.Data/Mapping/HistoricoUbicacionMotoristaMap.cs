using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuteoPedidos.Core.Model.Motoristas;

namespace RuteoPedidos.Data.Mapping
{
    public class HistoricoUbicacionMotoristaMap: IEntityTypeConfiguration<HistoricoUbicacionMotorista>
    {
        public void Configure(EntityTypeBuilder<HistoricoUbicacionMotorista> builder)
        {
            builder.ToTable(nameof(HistoricoUbicacionMotorista));

            builder.HasKey(x => x.Codigo);

            builder.Property(x => x.Codigo).HasMaxLength(100);
            builder.Property(x => x.Latitud).HasColumnType("decimal(9,6)");
            builder.Property(x => x.Longitud).HasColumnType("decimal(9,6)");

            builder.HasOne(x => x.Motorista).WithMany().HasForeignKey(f => f.IdMotorista);

        }
    }
}
