using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parqueadero.Models;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Controllers.Api;

[Authorize(Roles = "SuperUsuario,Administrador")]
[Route("api/Parqueaderos")]
[ApiController]
public class ParqueaderosApiController : ControllerBase
{
    private readonly IParqueaderoServicio _parqueaderoServicio;

    public ParqueaderosApiController(IParqueaderoServicio parqueaderoServicio)
    {
        _parqueaderoServicio = parqueaderoServicio;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.Parqueadero>>> ObtenerTodos()
    {
        var user = User;
        var parqueaderos = user.IsInRole("SuperUsuario") 
            ? await _parqueaderoServicio.ObtenerTodo()
            : await _parqueaderoServicio.ObtenerTodoPorEmpresa(int.Parse(user.FindFirst("EmpresaId")?.Value!));
        return Ok(parqueaderos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Models.Parqueadero>> ObtenerPorId(int id)
    {
        var parqueadero = await _parqueaderoServicio.ObtenerPorId(id);
        if (parqueadero == null)
            return NotFound();

        return Ok(parqueadero);
    }

    [HttpPost]
    public async Task<ActionResult<Models.Parqueadero>> Crear(Models.Parqueadero parqueadero)
    {
        try
        {
            var parqueaderoCreado = await _parqueaderoServicio.Insertar(parqueadero);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = parqueaderoCreado.Id }, parqueaderoCreado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, Models.Parqueadero parqueadero)
    {
        if (id != parqueadero.Id)
            return BadRequest();

        try
        {
            _parqueaderoServicio.Actualizar(parqueadero);
            return NoContent();
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
            if (await _parqueaderoServicio.Eliminar(id))
                return NoContent();
            return NotFound(new { mensaje = "No se encontró el parqueadero." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
} 