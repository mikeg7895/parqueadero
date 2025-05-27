using Microsoft.AspNetCore.Mvc;
using Parqueadero.Models;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Controllers.Api;

[ApiController]
[Route("api/Zonas")]
public class ZonasApiController : ControllerBase
{
    private readonly IZonaServicio _zonasServicio;

    public ZonasApiController(IZonaServicio zonasServicio)
    {
        _zonasServicio = zonasServicio;
    }

    [HttpGet("{pisoId}")]
    public async Task<ActionResult<IEnumerable<Zona>>> ObtenerTodosPorPiso(int pisoId)
    {
        return Ok(await _zonasServicio.ObtenerTodosPorPiso(pisoId));
    }

    [HttpGet("TipoVehiculo/{placa}/Piso/{pisoId}")]
    public async Task<ActionResult<IEnumerable<Zona>>> ObtenerTodosPorTipoVehiculo(string placa, int pisoId)
    {
        return Ok(await _zonasServicio.ObtenerTodosPorTipoVehiculoYPiso(placa, pisoId));
    }

    [HttpPost]
    public async Task<ActionResult<Zona>> Insertar([FromBody] Zona zona)
    {
        try
        {
            if (!Enum.IsDefined(zona.TipoVehiculo))
            {
                return BadRequest(new { mensaje = "Valor de tipo de vehículo inválido" });
            }

            var zonaCreada = await _zonasServicio.Insertar(zona);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = zonaCreada.Id }, zonaCreada);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpGet("ObtenerPorId/{id}")]
    public async Task<ActionResult<Zona>> ObtenerPorId(int id)
    {
        var zona = await _zonasServicio.ObtenerPorId(id);
        if (zona == null)
            return NotFound();

        return Ok(zona);
    }

    [HttpPut("{id}")]
    public ActionResult<Zona> Actualizar(int id, [FromBody] Zona zona)
    {
        try
        {
            if (!Enum.IsDefined(zona.TipoVehiculo))
            {
                return BadRequest(new { mensaje = "Valor de tipo de vehículo inválido" });
            }

            _zonasServicio.Actualizar(zona);
            return Ok(zona);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        try
        {
            if (await _zonasServicio.Eliminar(id))
                return NoContent();
            return NotFound(new { mensaje = "No se encontró la zona." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
