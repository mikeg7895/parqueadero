using Microsoft.EntityFrameworkCore;
using Parqueadero.Models;
using Parqueadero.Repositories.Interfaces;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Services;

public class TarifaServicio : GenericoServicio<Tarifa>, ITarifaServicio
{
    public TarifaServicio(ITarifaRepositorio repositorio) : base(repositorio)
    {
    }

    public async Task<IEnumerable<Tarifa>> ObtenerTarifasPorParqueadero(int parqueaderoId)
        => await ((ITarifaRepositorio)_repositorio).ObtenerTarifasPorParqueadero(parqueaderoId).ToListAsync();
}