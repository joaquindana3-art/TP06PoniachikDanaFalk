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
        bd.crearUsuario(nombre);
        int id = bd.ultimoUsuario();
        HttpContext.Session.SetString(id.ToString(), nombre);
        Random rnd = new Random();
        int numeroAleatorio = rnd.Next(1, 31);
        ViewBag.palabra = bd.palabraAhorcado(numeroAleatorio);
        return View("PrimeraHabitacion");

    }

    public IActionResult SegundaHabitacion()
    {
        return View();
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
