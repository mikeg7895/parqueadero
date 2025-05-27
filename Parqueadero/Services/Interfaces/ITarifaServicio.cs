using Parqueadero.Models;

namespace Parqueadero.Services.Interfaces;

public interface ITarifaServicio : IGenericoServicio<Tarifa>
{
    Task<IEnumerable<Tarifa>> ObtenerTarifasPorParqueadero(int parqueaderoId);
    Task<Tarifa?> ObtenerPorTipoDeVehiculo(TipoVehiculo tipoVehiculo);
}
