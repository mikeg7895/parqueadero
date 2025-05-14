using Parqueadero.Models;

namespace Parqueadero.Repositories.Interfaces;

public interface IVehiculoRepositorio : IGenericoRepositorio<Vehiculo>
{
    Vehiculo? ObtenerPorPlaca(string placa);
}