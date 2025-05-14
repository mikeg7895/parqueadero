using Parqueadero.Models;
using Parqueadero.Repositories.Interfaces;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Services;

public class VehiculoServicio : GenericoServicio<Vehiculo>, IVehiculoServicio
{
    public VehiculoServicio(IVehiculoRepositorio repositorio) : base(repositorio)
    {
    }
}
