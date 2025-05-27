using Parqueadero.Models;

namespace Parqueadero.Repositories.Interfaces;

public interface ITarifaRepositorio : IGenericoRepositorio<Tarifa>
{
    IQueryable<Tarifa> ObtenerTarifasPorParqueadero(int parqueaderoId);
    Task<Tarifa?> ObtenerPorTipoDeVehiculo(TipoVehiculo tipoVehiculo);
}
