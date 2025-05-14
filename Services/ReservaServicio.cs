using Parqueadero.Models;
using Parqueadero.Repositories.Interfaces;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Services;

public class ReservaServicio : GenericoServicio<Reserva>, IReservaServicio
{
    public ReservaServicio(IReservaRepositorio repositorio) : base(repositorio)
    {
    }

    public async Task<Reserva?> ObtenerPorPlacaConEstadoActivo(string placa)
        => await ((IReservaRepositorio)_repositorio).ObtenerPorPlacaConEstadoActivo(placa);
}