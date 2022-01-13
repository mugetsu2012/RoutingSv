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
    public class VisitaRepository: IVisitaRepository
    {
        private readonly RuteoPedidosContext _context;

        public VisitaRepository(RuteoPedidosContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteVisitaParaTareaAsync(int idTarea)
        {
            return await _context.Visitas.AnyAsync(x => x.IdTarea == idTarea);
        }

        public async Task<Visita> AgregarVisitaAsync(Visita visita)
        {
            await _context.Visitas.AddAsync(visita);

            return visita;
        }

        public async Task<List<Visita>> GetVisitasByTareasAsync(int[] idsTareas)
        {
            return await _context.Visitas.Where(x => idsTareas.Any(y => y == x.IdTarea)).ToListAsync();
        }
    }
}
