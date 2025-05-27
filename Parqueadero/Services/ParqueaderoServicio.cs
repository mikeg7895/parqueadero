using Microsoft.EntityFrameworkCore;
using Parqueadero.Repositories.Interfaces;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Services;

public class ParqueaderoServicio : GenericoServicio<Models.Parqueadero>, IParqueaderoServicio
{
    private readonly IPisoServicio _pisosServicio;
    public ParqueaderoServicio(IParqueaderoRepositorio parqueaderoRepository, IPisoServicio pisosServicio) : base(parqueaderoRepository)
    {
        _pisosServicio = pisosServicio;
    }

    public async Task<IEnumerable<Models.Parqueadero>> ObtenerTodoPorEmpresa(int empresaId) => await ((IParqueaderoRepositorio)_repositorio).ObtenerTodoPorEmpresa(empresaId).ToListAsync();

    public override async Task<Models.Parqueadero> Insertar(Models.Parqueadero parqueadero)
    {
        var nuevoValor = await _repositorio.Insertar(parqueadero);
        var nuevoPiso = new Models.Piso
        {
            ParqueaderoId = parqueadero.Id,
            Numero = 1
        };
        await _pisosServicio.Insertar(nuevoPiso);
        return nuevoValor;
    }
}