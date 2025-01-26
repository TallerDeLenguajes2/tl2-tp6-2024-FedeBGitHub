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

        try
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
        catch(Exception ex){
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar la página";
            return View("Index");
        }
    }

    [HttpPost]
    public IActionResult Logeo(LogeoViewModel usuario)
    {
        try
        {
            //En caso de que el usuario no escriba el usuario o la contraseña mando este mensaje de error (No va a pasar porque tengo puesto los requerid en los campos del formulario)
            if (string.IsNullOrEmpty(usuario.NomUsuario) || string.IsNullOrEmpty(usuario.Contrasenia))
            {
                usuario.ErrorMessage = "Por favor ingrese su nombre de usuario y contraseña.";
                return View("Index", usuario);
            }

            Usuario user = _usuarioRepository.obtenerUsuario(usuario.NomUsuario,usuario.Contrasenia);
            
            if (user != null)
            {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                HttpContext.Session.SetString("NomUsuario", user.NomUsuario);
                HttpContext.Session.SetString("Rol", user.Rol.ToString());
                _logger.LogInformation("El usuario " + user.NomUsuario + " ingresó correctamente");
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }
            
            
            return View("Index", usuario); //nunca llega aqui
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Intento de acceso invalido - Usuario: " + usuario.NomUsuario + " | Clave ingresada: " + usuario.Contrasenia);
            _logger.LogError(ex.ToString());
            //return RedirectToAction("Index", usuario); No funca asi
            usuario.ErrorMessage = "Credenciales Iválidas";
            return View("Index", usuario);
        }
        
    }

    public IActionResult CerrarSesion()
    {
        try
        {
            HttpContext.Session.Clear(); // Borra todos los datos de la sesión
            return RedirectToAction("Index", "Logeo");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo cerrar la sesión";
            return View("Index"); 
        }
        
    }
}