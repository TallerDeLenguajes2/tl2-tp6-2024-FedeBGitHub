using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_FedeBGitHub.Models;
using repositorys;

//namespace models.Controllers;

namespace tl2_tp6_2024_FedeBGitHub.Controllers;

public class ClienteController : Controller
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ILogger<ClienteController> _logger;

    public ClienteController(ILogger<ClienteController> logger,IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
        _logger = logger;
    }

    
    [HttpGet]
    public IActionResult ListarCliente()
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) return RedirectToAction ("Index", "Logeo");
            if (HttpContext.Session.GetString("Rol") == Rol.Admin.ToString())
            {
                List<Cliente> listaClientes = _clienteRepository.listarClientes();
                return View(listaClientes);
            }else{
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo cargar la lista de clientes";
            return View(new List<Cliente>());
        }
        
    }

    [HttpGet]
    public IActionResult CrearCliente()
    {
        try
        {
            if (HttpContext.Session.GetString("IsAuthenticated") != null &&
                HttpContext.Session.GetString("Rol") == Rol.Admin.ToString())
            {
                return View(new AltaClienteViewModel());
            }else{
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo cargar el formulario para agregar clientes";
            return View("ListarCliente", new List<Cliente>());
        }
    }

    [HttpPost]
    public IActionResult CrearClientePost(AltaClienteViewModel c)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("ListarCliente");
            }
            Cliente cliente = new Cliente(c.Nombre,c.Email,c.Telefono);
            _clienteRepository.CrearCliente(cliente);
            return RedirectToAction("ListarCliente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo agregar cliente";
            return View("ListarCliente", new List<Cliente>());
        }
    }

    [HttpGet]
    public IActionResult ModificarCliente(ModificarClienteViewModel cliente)
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) 
            {
                return RedirectToAction("Index", "Logeo");
            } 

            if (HttpContext.Session.GetString("Rol") != Rol.Admin.ToString())
            {
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }
            return View(cliente);
         }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo cargar el formulario para modificar cliente";
            return View("ListarCliente", new List<Cliente>());
           
        }
    }

    [HttpPost]
    public IActionResult ModificarClientePost(ModificarClienteViewModel c)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("ListarCliente");
            }
            Cliente cliente = new Cliente(c.ClienteId,c.Nombre,c.Email,c.Telefono);
            _clienteRepository.modificarCliente(cliente);
            return RedirectToAction("ListarCliente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo modificar cliente";
            return View("ListarCliente", new List<Cliente>());
        }
    }

    [HttpGet]
    public IActionResult EliminarCliente(Cliente cliente)
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) 
                return RedirectToAction("Index", "Logeo");

            if (HttpContext.Session.GetString("Rol") != Rol.Admin.ToString())
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            
            return View(cliente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo cargar el formulario para eliminar cliente";
            return View("ListarCliente", new List<Cliente>());
           
        }
        
    }

    [HttpPost]
    public IActionResult EliminarClientePost(int ClienteId)
    {
        try
        {
            _clienteRepository.EliminarCliente(ClienteId);
            return RedirectToAction("ListarCliente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo eliminar cliente";
            return View("ListarCliente", new List<Cliente>());
            
        }
    }
}
