using Parqueadero.Models;
using Parqueadero.Repositories.Interfaces;

namespace Parqueadero.Repositories;

public class TarifaRepositorio : GenericoRepositorio<Tarifa>, ITarifaRepositorio
{
    public TarifaRepositorio(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Tarifa> ObtenerTarifasPorParqueadero(int parqueaderoId)
        => Entities.Where(t => t.ParqueaderoId == parqueaderoId);
}
