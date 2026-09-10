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
            string respuestaCorrecta = palabrasRosco[i].respuesta.ToUpper();

            if ( respuesta.ToUpper() == respuestaCorrecta )
            {
                palabrasCorrectas++;
                i++;
                return CargarSegundaHabitacion(i, palabrasCorrectas, palabrasIncorrectas, "Correcto");
            }

            palabrasIncorrectas++;
            return CargarSegundaHabitacion(i, palabrasCorrectas, palabrasIncorrectas, "Incorrecto");
        }
        if (palabrasCorrectas >= palabrasIncorrectas)
        {
            return CargarSegundaHabitacion(i, palabrasCorrectas, palabrasIncorrectas, "Ganaste");
        }
        else
        {
            return CargarSegundaHabitacion(i, palabrasCorrectas, palabrasIncorrectas, "Perdiste");
        }

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

    public IActionResult TerceraHabitacion()
    {
        return CargarTerceraHabitacion(0, 0, "");
    }

    [HttpPost]
    public IActionResult TerceraHabitacion(string respuesta, int i, int aciertos)
    {
        BD bd = new BD();
        List<Adivinanzas> adivinanzas = bd.adivinanzas();

        if (adivinanzas.Count == 0)
        {
            return CargarTerceraHabitacion(0, 0, "No hay adivinanzas cargadas en la base de datos.");
        }

        if (i < 0)
        {
            i = 0;
        }

        if (i >= adivinanzas.Count)
        {
            return CargarTerceraHabitacion(i, aciertos, "¡Completaste todas las adivinanzas!");
        }

        if (!string.IsNullOrWhiteSpace(respuesta) && respuesta.Trim().Equals(adivinanzas[i].respuesta.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            aciertos++;
            i++;

            if (i >= adivinanzas.Count)
            {
                return CargarTerceraHabitacion(i, aciertos, "¡Correcto! Completaste todas las adivinanzas.");
            }

            return CargarTerceraHabitacion(i, aciertos, "¡Correcto! Pasaste a la siguiente adivinanza.");
        }

        return CargarTerceraHabitacion(i, aciertos, "Incorrecto. Intenta otra vez.");
    }

    private IActionResult CargarTerceraHabitacion(int i, int aciertos, string resultado)
    {
        BD bd = new BD();
        List<Adivinanzas> adivinanzas = bd.adivinanzas();

        ViewBag.adivinanzas = adivinanzas;
        ViewBag.i = i;
        ViewBag.aciertos = aciertos;
        ViewBag.resultado = resultado;
        return View("TerceraHabitacion");
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
