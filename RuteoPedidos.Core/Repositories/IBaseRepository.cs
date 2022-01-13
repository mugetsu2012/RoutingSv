using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RuteoPedidos.Core.Repositories
{
    public interface IBaseRepository
    {
        Task<int> SaveChangesAsync();
    }
}
