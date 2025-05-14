using Microsoft.EntityFrameworkCore;
using Parqueadero.Models;
using Parqueadero.Repositories.Interfaces;

namespace Parqueadero.Repositories;

public class UsuarioRepositorio : GenericoRepositorio<Usuario>, IUsuarioRepositorio
{
    public UsuarioRepositorio(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> ObtenerPorEmail(string email) => await Entities.FirstOrDefaultAsync(u => u.Correo == email);

    public override IQueryable<Usuario> ObtenerTodo() => Entities.Include(u => u.Empresa);

    public IQueryable<Usuario> ObtenerTodosPorEmpresa(int empresaId) => Entities.Where(u => u.EmpresaId == empresaId);
    
}