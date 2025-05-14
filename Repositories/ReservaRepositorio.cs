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
}