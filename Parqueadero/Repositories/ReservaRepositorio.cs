using Microsoft.EntityFrameworkCore;
using Parqueadero.Models;
using Parqueadero.Repositories.Interfaces;

namespace Parqueadero.Repositories;

public class ReservaRepositorio : GenericoRepositorio<Reserva>, IReservaRepositorio
{
    public ReservaRepositorio(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Reserva?> ObtenerPorPlacaConEstadoActivo(string placa)
        => await Entities
            .Include(r => r.Vehiculo)
            .Where(r => r.Vehiculo != null && r.Vehiculo.Placa == placa && r.Estado == EstadoReserva.Activa)
            .FirstOrDefaultAsync();

    public override IQueryable<Reserva> ObtenerTodo()
        => Entities
            .Include(r => r.Vehiculo)
            .Include(r => r.Zona)
            .Include(r => r.Usuario);

    public override async Task<Reserva?> ObtenerPorId(int id)
        => await Entities
            .Include(r => r.Vehiculo)
            .Include(r => r.Zona)
            .Include(r => r.Usuario)
                .ThenInclude(u => u.PlanEspecial)
            .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<bool> CancelarReserva(int id)
    {
        var reserva = await ObtenerPorId(id) ?? throw new Exception("Reserva no encontrada");
        reserva.Estado = EstadoReserva.Cancelada;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> FinalizarReserva(int id)
    {
        var reserva = await ObtenerPorId(id) ?? throw new Exception("Reserva no encontrada");
        reserva.Estado = EstadoReserva.Finalizada;
        reserva.HoraSalida = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}