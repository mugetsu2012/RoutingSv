using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Data.Mapping
{
    public class HistoricoTareasOrdenamientoMap: IEntityTypeConfiguration<HistoricoTareasOrdenamiento>
    {
        public void Configure(EntityTypeBuilder<HistoricoTareasOrdenamiento> builder)
        {
            builder.ToTable(nameof(HistoricoTareasOrdenamiento));

            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.CodigoTareasInvolucradas).HasConversion(v => string.Join(";", v),
                v => Array.ConvertAll(v.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries), int.Parse));

            builder.HasOne(x => x.Motorista).WithMany().HasForeignKey(f => f.IdMotorista);
        }
    }
}
