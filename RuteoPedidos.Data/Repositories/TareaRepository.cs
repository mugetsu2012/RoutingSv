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
    public class TareaRepository: ITareaRepository
    {
        private readonly RuteoPedidosContext _context;

        public TareaRepository(RuteoPedidosContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Tarea> AgregarTareaAsync(Tarea tarea)
        {
            await _context.Tareas.AddAsync(tarea);
            return tarea;
        }

        public async Task<Tarea> GetTareaByIdAsync(int idTarea, bool track = false)
        {
            Tarea tarea;

            if (track)
            {
                tarea = await _context.Tareas.FirstOrDefaultAsync(x => x.Codigo == idTarea);
            }
            else
            {
                tarea = await _context.Tareas.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == idTarea);
            }

            return tarea;
        }

        public void MarcarEliminarTarea(int idTarea)
        {
            Tarea tarea = new Tarea()
            {
                Codigo = idTarea
            };

            _context.Entry(tarea).State = EntityState.Deleted;
        }

        public async Task<List<Tarea>> GetTareasAsync(Expression<Func<Tarea, bool>> where, int skip, int pageSize, bool agregarIncludes = false)
        {
            List<Tarea> tareas;

            if (agregarIncludes)
            {
                tareas =
                    await _context.Tareas.AsNoTracking()
                        .Include(x => x.Motorista)
                        .Where(where).Skip(skip)
                        .Take(pageSize)
                        .ToListAsync();
            }
            else
            {
                tareas =
                    await _context.Tareas.AsNoTracking().Where(where).Skip(skip).Take(pageSize).ToListAsync();
            }

            return tareas;
        }

        public async Task<int> CountTareasAsync(Expression<Func<Tarea, bool>> where)
        {
            int conteo = await _context.Tareas.CountAsync(where);
            return conteo;
        }
    }
}
