using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RuteoPedidos.Core.Model.Motoristas;
using RuteoPedidos.Core.Repositories;

namespace RuteoPedidos.Data.Repositories
{
    public class MotoristaRepository: IMotoristaRepository
    {
        private readonly RuteoPedidosContext _context;

        public MotoristaRepository(RuteoPedidosContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            int count = await _context.SaveChangesAsync();
            return count;
        }

        public async Task<Motorista> AgregarEditarMotoristaAsync(Motorista motorista)
        {
            if (motorista.Codigo == 0)
            {
                await _context.Motoristas.AddAsync(motorista);
            }

            _context.Entry(motorista).State = EntityState.Modified;

            return motorista;
        }

        public async Task<Motorista> GetMotoristaByIdAsync(int idMotorista, bool trackear = false)
        {
            Motorista motorista;
            if (trackear)
            {
                motorista = await _context.Motoristas.FirstOrDefaultAsync(x => x.Codigo == idMotorista);
            }
            else
            {
                motorista = await _context.Motoristas.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == idMotorista);
            }

            return motorista;
        }

        public async Task<List<Motorista>> GetMotoristasAsync(Expression<Func<Motorista, bool>> where, int skip, int itemsPerPage)
        {
            List<Motorista> motoristas = await _context.Motoristas.AsNoTracking().Where(where).Skip(skip).Take(itemsPerPage).ToListAsync();
            return motoristas;
        }

        public async Task<int> CountMotoristasAsync(Expression<Func<Motorista, bool>> where)
        {
            int cantidad = await _context.Motoristas.CountAsync(where);
            return cantidad;
        }

        public void MarcarMotoristaModificado(Motorista motorista)
        {
            _context.Entry(motorista).State = EntityState.Modified;
        }
    }
}
