using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parqueadero.Models;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Controllers.Api;

[Authorize(Roles = "Administrador,SuperUsuario,Trabajador")]
[Route("api/Reservas")]
[ApiController]
public class ReservasApiController : ControllerBase
{
    private readonly IReservaServicio _reservaServicio;

    public ReservasApiController(IReservaServicio reservaServicio)
    {
        _reservaServicio = reservaServicio;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reserva>>> ObtenerTodas()
    {
        var reservas = await _reservaServicio.ObtenerTodo();
        return Ok(reservas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reserva>> ObtenerPorId(int id)
    {
        var reserva = await _reservaServicio.ObtenerPorId(id);
        if (reserva == null)
        {
            return NotFound();
        }
        return Ok(reserva);
    }

    [HttpPost]
    public async Task<ActionResult<Reserva>> Crear([FromBody] Reserva reserva)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            reserva.HoraEntrada = DateTime.UtcNow;
            reserva.Estado = EstadoReserva.Activa;
            var reservaCreada = await _reservaServicio.Insertar(reserva);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = reservaCreada.Id }, reservaCreada);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, [FromBody] Reserva reserva)
    {
        if (id != reserva.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _reservaServicio.Actualizar(reserva);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("{id}/Cancelar")]
    public async Task<IActionResult> CancelarReserva(int id)
    {
        try
        {
            if (await _reservaServicio.CancelarReserva(id))
                return NoContent();
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("{id}/finalizar")]
    public async Task<IActionResult> FinalizarReserva(int id)
    {
        try
        {
            var reserva = await _reservaServicio.ObtenerPorId(id);
            if (reserva == null)
            {
                return NotFound();
            }

            if (reserva.Estado == EstadoReserva.Finalizada)
            {
                return BadRequest(new { mensaje = "La reserva ya está finalizada" });
            }

            reserva.Estado = EstadoReserva.Finalizada;
            reserva.HoraSalida = DateTime.Now;
            _reservaServicio.Actualizar(reserva);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
