using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RuteoPedidos.Core.Model.Tareas;
using RuteoPedidos.Core.Repositories;

namespace RuteoPedidos.Data.Repositories
{
    public class DestinoRepository: IDestinoRepository
    {
        private readonly RuteoPedidosContext _context;

        public DestinoRepository(RuteoPedidosContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Destino> AgregarDestinoAsync(Destino destino)
        {
            await _context.Destinos.AddAsync(destino);
            return destino;
        }

        public async Task<List<Destino>> BuscarDestinosAsync(Expression<Func<Destino, bool>> where, int skip, int pageSize)
        {
            List<Destino> destinos =
                await _context.Destinos.AsNoTracking().Where(where).Skip(skip).Take(pageSize).ToListAsync();

            return destinos;
        }

        public async Task<int> CountDestinoAsync(Expression<Func<Destino, bool>> where)
        {
            int count = await _context.Destinos.CountAsync(where);
            return count;
        }

        public async Task<Destino> GetDestinoByIdAsync(int idDestino, bool track = false)
        {
            Destino destino;

            if (track)
            {
                destino = await _context.Destinos.FirstOrDefaultAsync(x => x.Codigo == idDestino);
            }
            else
            {
                destino = await _context.Destinos.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == idDestino);
            }

            return destino;
        }
    }
}
