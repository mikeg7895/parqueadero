using Microsoft.EntityFrameworkCore;
using Parqueadero.Models;
using Parqueadero.Repositories.Interfaces;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Services;

public class ZonaServicio : GenericoServicio<Zona>, IZonaServicio
{
    public ZonaServicio(IZonaRepositorio repositorio) : base(repositorio)
    {
    }

    public async Task<IEnumerable<Zona>> ObtenerTodosPorPiso(int pisoId) 
        => await ((IZonaRepositorio)_repositorio).ObtenerTodosPorPiso(pisoId).ToListAsync();
}