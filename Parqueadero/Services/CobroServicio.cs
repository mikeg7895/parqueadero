using Parqueadero.Factories.FacturaFactory;
using Parqueadero.Models;
using Parqueadero.Repositories.Interfaces;
using Parqueadero.Services.Interfaces;
using Parqueadero.Strategies.Descuento;
using Parqueadero.Strategies.Tarifa;

namespace Parqueadero.Services;

public class CobroServicio : ICobroServicio
{
    private readonly IReservaServicio _reservaServicio;
    private readonly IDescuentoServicio _descuentoServicio;
    private readonly ITarifaServicio _tarifaServicio;
    private readonly IEnumerable<IDescuentoStrategy> _descuentoStrategy;
    private readonly IEnumerable<ITarifaStrategy> _tarifaStrategy;
    private readonly ICobroRepositorio _cobroRepositorio;

    public CobroServicio(IReservaServicio reservaServicio, IDescuentoServicio descuentoServicio, ITarifaServicio tarifaServicio, IEnumerable<IDescuentoStrategy> descuentoStrategy, IEnumerable<ITarifaStrategy> tarifaStrategy, ICobroRepositorio cobroRepositorio)
    {
        _reservaServicio = reservaServicio;
        _descuentoServicio = descuentoServicio;
        _tarifaServicio = tarifaServicio;
        _descuentoStrategy = descuentoStrategy;
        _tarifaStrategy = tarifaStrategy;
        _cobroRepositorio = cobroRepositorio;
    }

    public async Task<Cobro> GenerarCobro(int idReserva, string? codigoDescuento)
    {
        var reserva = await _reservaServicio.ObtenerPorId(idReserva) ?? throw new ArgumentException("Reserva no encontrada");
        var tarifa = await _tarifaServicio.ObtenerPorTipoDeVehiculo(reserva.Vehiculo!.TipoVehiculo) ?? throw new ArgumentException("Tarifa no encontrada");
        Cobro cobro;
        if (reserva.Usuario!.PlanEspecial is not null)
        {
            cobro = new Cobro
            {
                ReservaId = idReserva,
                TarifaId = tarifa.Id,
                DescuentoId = null,
                Total = reserva.Usuario.PlanEspecial.Costo,
                FechaCobro = DateTime.UtcNow
            };
        } else
        {
            var tarifaStrategy = _tarifaStrategy.FirstOrDefault(ts => ts is TarifaPorHoraStrategy);

            await _reservaServicio.ActualizarHoraSalida(idReserva);
            var subtotal = tarifaStrategy!.Calcular(tarifa, reserva);
            IDescuentoStrategy descuentoStrategy;
            Descuento? descuento = null;
            if (codigoDescuento is not null)
            {
                descuento = await _descuentoServicio.BuscarPorCodigo(codigoDescuento) ?? throw new ArgumentException("Descuento no encontrado");
                descuentoStrategy = _descuentoStrategy.FirstOrDefault(ds => ds is CodigoDescuentoStrategy);
                if (descuentoStrategy is null)
                    throw new ArgumentException("Strategy no encontrado");
                if (!descuentoStrategy.EsValido(descuento))
                    throw new ArgumentException("Descuento no valido");
                subtotal = descuentoStrategy!.Aplicar(descuento, subtotal);
            }
            cobro = new Cobro
            {
                ReservaId = idReserva,
                TarifaId = tarifa.Id,
                DescuentoId = descuento?.Id,
                Total = subtotal,
                FechaCobro = DateTime.UtcNow
            };
        }
        var cobroCreado = await _cobroRepositorio.Insertar(cobro);
        await _reservaServicio.FinalizarReserva(idReserva);
        return cobroCreado;
    }
}
