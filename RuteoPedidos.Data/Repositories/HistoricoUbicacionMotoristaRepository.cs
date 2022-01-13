using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RuteoPedidos.Core.Model.Motoristas;
using RuteoPedidos.Core.Repositories;

namespace RuteoPedidos.Data.Repositories
{
    public class HistoricoUbicacionMotoristaRepository: IHistoricoUbicacionMotoristaRepository
    {
        private readonly RuteoPedidosContext _context;

        public HistoricoUbicacionMotoristaRepository(RuteoPedidosContext context)
        {
            _context = context;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<HistoricoUbicacionMotorista> AgregarHistoricoAsync(HistoricoUbicacionMotorista historicoUbicacionMotorista)
        {
            await _context.HistoricoUbicacionMotoristas.AddAsync(historicoUbicacionMotorista);

            return historicoUbicacionMotorista;
        }
    }
}
