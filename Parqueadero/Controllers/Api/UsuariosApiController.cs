using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parqueadero.Models;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Controllers.Api;

[Authorize(Roles = "SuperUsuario,Administrador,Trabajador")]
[Route("api/Usuarios")]
[ApiController]
public class UsuariosApiController : ControllerBase
{
    private readonly IUsuarioServicio _usuarioServicio;
    private readonly IAutenticacionServicio _autenticacionServicio;

    public UsuariosApiController(IUsuarioServicio usuarioServicio, IAutenticacionServicio autenticacionServicio)
    {
        _usuarioServicio = usuarioServicio;
        _autenticacionServicio = autenticacionServicio;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerTodos()
    {
        var empresaId = User.FindFirst("EmpresaId")?.Value;
        var usuarios = User.IsInRole("SuperUsuario") 
            ? await _usuarioServicio.ObtenerTodo() 
            : await _usuarioServicio.ObtenerTodosPorEmpresa(int.Parse(empresaId!));
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> ObtenerPorId(int id)
    {
        var usuario = await _usuarioServicio.ObtenerPorId(id);
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> Crear([FromBody] Usuario usuario)
    {
        try
        {
            if (!Enum.IsDefined(usuario.Rol))
            {
                return BadRequest(new { mensaje = "Valor de rol inválido" });
            }

            var usuarioCreado = await _autenticacionServicio.RegistrarUsuario(usuario);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = usuarioCreado.Id }, usuarioCreado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, Usuario usuario)
    {
        if (id != usuario.Id)
            return BadRequest();

        try
        {
            await _usuarioServicio.ActualizarAsync(usuario);
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
            if (await _usuarioServicio.Eliminar(id))
                return NoContent();
            return NotFound(new { mensaje = "No se encontró el usuario." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("{id}/PlanEspecial/{planEspecialId}")]
    public async Task<IActionResult> AsignarPlanEspecial(int id, int planEspecialId)
    {
        try
        {
            if (planEspecialId == 0)
                await _usuarioServicio.AsignarPlanEspecial(id, null);
            else
                await _usuarioServicio.AsignarPlanEspecial(id, planEspecialId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}