using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parqueadero.Models;
using Parqueadero.Services.Interfaces;

namespace Parqueadero.Controllers.Api;

[Authorize(Roles = "Administrador,SuperUsuario,Trabajador")]
[Route("api/Facturas")]
[ApiController]
public class FacturaApiController : ControllerBase
{
    private readonly IReciboServicio _reciboServicio;

    public FacturaApiController(IReciboServicio reciboServicio)
    {
        _reciboServicio = reciboServicio;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recibo>>> ObtenerTodas()
    {
        var facturas = await _reciboServicio.ObtenerTodo();
        return Ok(facturas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recibo>> ObtenerPorId(int id)
    {
        var factura = await _reciboServicio.ObtenerPorId(id);
        if (factura == null)
        {
            return NotFound();
        }
        return Ok(factura);
    }

    [HttpGet("{id}/comprobante")]
    public async Task<IActionResult> DescargarComprobante(int id)
    {
        try
        {
            var (comprobante, contentType, extension) = await _reciboServicio.GenerarComprobante(id, "pdf");

            return File(comprobante, contentType, $"comprobante.{extension}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error al generar el comprobante", message = ex.Message });
        }
    }
}
