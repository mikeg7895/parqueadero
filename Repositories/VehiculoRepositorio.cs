using Parqueadero.Models;
using Parqueadero.Repositories.Interfaces;

namespace Parqueadero.Repositories;

public class VehiculoRepositorio : GenericoRepositorio<Vehiculo>, IVehiculoRepositorio
{
    public VehiculoRepositorio(ApplicationDbContext context) : base(context)
    {
    }

    public Vehiculo? ObtenerPorPlaca(string placa) 
        => Entities.FirstOrDefault(v => v.Placa == placa);
}
