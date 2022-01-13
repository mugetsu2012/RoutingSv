using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RuteoPedidos.Core.Model.Tareas;
using RuteoPedidos.Core.Repositories;

namespace RuteoPedidos.Data.Repositories
{
    public class HistoricoTareasOrdenamientoRepository: IHistoricoTareasOrdenamientoRepository
    {
        private readonly RuteoPedidosContext _context;

        public HistoricoTareasOrdenamientoRepository(RuteoPedidosContext context)
        {
            _context = context;
        }

        public async Task<HistoricoTareasOrdenamiento> GuardarHistoricoTareaAsync(HistoricoTareasOrdenamiento historico)
        {
            await _context.HistoricoTareasOrdenamientos.AddAsync(historico);
            return historico;
        }

        public async Task<List<HistoricoTareasOrdenamiento>> GetHistoricosTareasOrdenamientoAsync(int[] codigosMotoristas)
        {
            List<HistoricoTareasOrdenamiento> historicoTareasOrdenamientos = await _context.HistoricoTareasOrdenamientos
                .Where(x => codigosMotoristas.Any(y => y == x.IdMotorista)).ToListAsync();

            return historicoTareasOrdenamientos;
        }

        public async Task<int> SaveChangesAsync()
        {
            int count = await _context.SaveChangesAsync();
            return count;
        }
    }
}
