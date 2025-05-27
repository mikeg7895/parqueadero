using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Parqueadero.Controllers;

[Authorize(Roles = "Administrador,SuperUsuario,Trabajador")]
public class ReservaController : Controller
{
    private readonly IConfiguration _configuration;

    public ReservaController(IConfiguration configuration) : base()
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Crear()
    {
        ViewBag.PlaterecognizerToken = _configuration["APITokens:platerecognizer"];
        return View();
    }

    public IActionResult Editar(int id)
    {
        ViewBag.Id = id;
        return View();
    }

    public IActionResult Eliminar(int id)
    {
        ViewBag.Id = id;
        return View();
    }
}
