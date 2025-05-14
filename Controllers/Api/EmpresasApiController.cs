using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parqueadero.Models;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Controllers.Api;

[Authorize(Roles = "SuperUsuario")]
[Route("api/Empresas")]
[ApiController]
public class EmpresasApiController : ControllerBase
{
    private readonly IEmpresaServicio _empresaServicio;

    public EmpresasApiController(IEmpresaServicio empresaServicio)
    {
        _empresaServicio = empresaServicio;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empresa>>> ObtenerTodas()
    {
        var empresas = await _empresaServicio.ObtenerTodo();
        return Ok(empresas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Empresa>> ObtenerPorId(int id)
    {
        var empresa = await _empresaServicio.ObtenerPorId(id);
        if (empresa == null)
            return NotFound();

        return Ok(empresa);
    }

    [HttpPost]
    public async Task<ActionResult<Empresa>> Crear(Empresa empresa)
    {
        try
        {
            var empresaCreada = await _empresaServicio.Insertar(empresa);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = empresaCreada.Id }, empresaCreada);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, Empresa empresa)
    {
        if (id != empresa.Id)
            return BadRequest();

        try
        {
            _empresaServicio.Actualizar(empresa);
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
            if (await _empresaServicio.Eliminar(id))
                return NoContent();
            return NotFound(new { mensaje = "No se encontró la empresa." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
} 