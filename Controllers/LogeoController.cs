using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_FedeBGitHub.Models;
using repositorys;

namespace tl2_tp6_2024_FedeBGitHub.Controllers;

public class LogeoController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<ClienteController> _logger;

    public LogeoController(ILogger<ClienteController> logger,IUsuarioRepository usaurioRepository)
    {
        _usuarioRepository = usaurioRepository;
        _logger = logger;
    }

    
    public IActionResult Index()
    {
        // Si no hay una sesion iniciada
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated")))
        {
            // Crear un nuevo ViewModel, pasamos el estado de autenticación
            var model = new LogeoViewModel
            {
                IsAuthenticated = HttpContext.Session.GetString("IsAuthenticated") == "true"
            };
            return View(model); // Pasamos el ViewModel con la propiedad de autenticación
        }else{ // si hay una sesion iniciada no muestra el formulario sino lo redirige a home
           return RedirectToAction("ListarCliente", "Cliente");
        }
    }

    [HttpPost]
    public IActionResult Logeo(LogeoViewModel usuario)
    {
        Usuario user = _usuarioRepository.obtenerUsuario(usuario.NomUsuario,usuario.Contrasenia);
        
        if (user != null)
        {
            HttpContext.Session.SetString("IsAuthenticated", "true");
            HttpContext.Session.SetString("NomUsuario", user.NomUsuario);
            HttpContext.Session.SetString("Rol", user.Rol.ToString());
            return RedirectToAction("ListarPresupuesto", "Presupuestos");
        }
       
        return RedirectToAction("Index", "Home");
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear(); // Borra todos los datos de la sesión
        return RedirectToAction("Index", "Logeo");
    }
}