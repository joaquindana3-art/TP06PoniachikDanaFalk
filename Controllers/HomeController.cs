using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06PoniachikDanaFalk.Models;

namespace TP06PoniachikDanaFalk.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult PartidaIniciada(string nombre) {

        BD bd = new BD();
        List<string> listaAhorcado = new List<string> {"MIDAS", "SKYE", "BRUTUS", "TNTINA", "MIAUSCULOS", "LA AGENCIA", "LA GRUTA", "EL YATE", "LA PLATAFORMA", "EL TIBURON", "GRAPPLER", "ARCO DE BOOM", "MINIGUN DE BRUTUS", "FUSIL DE ASALTO", "SUBFUSIL DE TAMBOR", "FUSIL PESADO", "SUBFUSIL DE MIDAS", "FUSIL DE SKYE", "ARMA LEGENDARIA", "ESCAPAR", "VICTORIA", "SECRETOS", "ESPIONAJE", "AGENTES", "DERROTAR", "TARJETA DE ACCESO", "BOVEDA", "ESPADAS", "EXPLOSION", "LLAVE", "ESCAPAR DE LA AGENCIA"};
        int idUsuario = bd.crearUsuario(nombre);
        HttpContext.Session.SetString(idUsuario.ToString(), nombre);
        Random rnd = new Random();
        int numeroAleatorio = rnd.Next(1, listaAhorcado.Count);
        ViewBag.palabra = listaAhorcado[numeroAleatorio];
        return View("PrimeraHabitacion");

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
