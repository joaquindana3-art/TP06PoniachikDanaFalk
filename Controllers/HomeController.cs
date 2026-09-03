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
        return CargarSegundaHabitacion(0, 0, 0, "");
    }

    [HttpPost]
    public IActionResult SegundaHabitacion(string respuesta, int i, int palabrasCorrectas, int palabrasIncorrectas)
    {
        BD bd = new BD();
        List<PalabrasRosco> palabrasRosco = bd.palabrasRosco();

        if (i >= 0 && i < palabrasRosco.Count)
        {
            string respuestaUsuario = (respuesta ?? "").Trim();
            string respuestaCorrecta = (palabrasRosco[i].respuesta ?? "").Trim();

            if (string.Equals(respuestaUsuario, respuestaCorrecta, StringComparison.OrdinalIgnoreCase))
            {
                palabrasCorrectas++;
                i++;
                return CargarSegundaHabitacion(i, palabrasCorrectas, palabrasIncorrectas, "Correcto");
            }

            palabrasIncorrectas++;
            return CargarSegundaHabitacion(i, palabrasCorrectas, palabrasIncorrectas, "Incorrecto");
        }

        return CargarSegundaHabitacion(i, palabrasCorrectas, palabrasIncorrectas, "Completaste el rosco");
    }

    private IActionResult CargarSegundaHabitacion(int i, int palabrasCorrectas, int palabrasIncorrectas, string resultado)
    {
        BD bd = new BD();
        ViewBag.palabrasRosco = bd.palabrasRosco();
        ViewBag.i = i;
        ViewBag.palabrasCorrectas = palabrasCorrectas;
        ViewBag.palabrasIncorrectas = palabrasIncorrectas;
        ViewBag.resultado = resultado;
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
