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

    public override IQueryable<Usuario> ObtenerTodo() => Entities.Include(u => u.Empresa).Include(u => u.PlanEspecial);

    public override async Task<Usuario?> ObtenerPorId(int id) => await Entities
        .Include(u => u.Empresa)
        .Include(u => u.PlanEspecial)
        .FirstOrDefaultAsync(u => u.Id == id);

    public IQueryable<Usuario> ObtenerTodosPorEmpresa(int empresaId) => Entities.Where(u => u.EmpresaId == empresaId).Include(u => u.PlanEspecial);

    public async Task AsignarPlanEspecial(int usuarioId, int? planEspecialId)
    {
        var usuario = await Entities.FirstOrDefaultAsync(u => u.Id == usuarioId);
        if (usuario == null)
            throw new Exception("Usuario no encontrado");

        usuario.PlanEspecialId = planEspecialId;
        await _context.SaveChangesAsync();
    }   

}