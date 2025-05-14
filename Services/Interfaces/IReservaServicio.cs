using Parqueadero.Models;

namespace Parqueadero.Services.Interfaces;

public interface IReservaServicio : IGenericoServicio<Reserva>
{
    Task<Reserva?> ObtenerPorPlacaConEstadoActivo(string placa);
}
