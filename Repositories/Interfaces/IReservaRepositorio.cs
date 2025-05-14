using Parqueadero.Models;

namespace Parqueadero.Repositories.Interfaces;

public interface IReservaRepositorio : IGenericoRepositorio<Reserva>
{
    Task<Reserva?> ObtenerPorPlacaConEstadoActivo(string placa);
}
