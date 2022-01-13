using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Repositories;

namespace RuteoPedidos.Data.Repositories
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly RuteoPedidosContext _context;

        public UsuarioRepository(RuteoPedidosContext context)
        {
            _context = context;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Usuario> CrearModificarUsuarioAsync(Usuario usuario, bool esNuevo)
        {

            if (esNuevo)
            {
                await _context.Usuarios.AddAsync(usuario);
            }
            else
            {
                _context.Entry(usuario).State = EntityState.Modified;
            }

            return usuario;
        }

        public async Task<Usuario> GetUsuarioAsync(string idUsuario, bool track = false)
        {
            Usuario usuario;

            if (track)
            {
                usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);
            }
            else
            {
                usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);
            }

            return usuario;
        }

        public async Task<List<Usuario>> GetUsuariosAsync(Expression<Func<Usuario, bool>> where, int skip, int top, bool agregarIncludes = false)
        {
            List<Usuario> usuarios;

            if (agregarIncludes)
            {
                usuarios =
                    await _context.Usuarios.AsNoTracking()
                        .Include(x => x.Cuenta)
                        .Where(where).Skip(skip)
                        .Take(top)
                        .ToListAsync();
            }
            else
            {
                usuarios =
                    await _context.Usuarios.AsNoTracking().Where(where).Skip(skip).Take(top).ToListAsync();
            }

            return usuarios;
        }

        public async Task<int> CountUsuariosAsync(Expression<Func<Usuario, bool>> where)
        {
            return await _context.Usuarios.CountAsync(where);
        }
    }
}
