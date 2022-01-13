using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Data.Mapping
{
    public class VisitaMap: IEntityTypeConfiguration<Visita>
    {
        public void Configure(EntityTypeBuilder<Visita> builder)
        {
            builder.ToTable(nameof(Visita));

            builder.HasKey(x => x.Codigo);

            builder.Property(x => x.Codigo).IsRequired().HasMaxLength(100);
            builder.Property(x => x.TextoResultadoVisita).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Comentario).IsRequired(false);

            builder.HasOne(x => x.Tarea).WithMany().HasForeignKey(f => f.IdTarea);
        }
    }
}
