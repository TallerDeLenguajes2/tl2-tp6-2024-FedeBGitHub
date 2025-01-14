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
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) return RedirectToAction ("Index", "Logeo");
        List<Cliente> listaClientes = _clienteRepository.listarClientes();
        return View(listaClientes);
    }

    [HttpGet]
    public IActionResult CrearCliente()
    {
        return View(new AltaClienteViewModel());
    }

    [HttpPost]
    public IActionResult CrearClientePost(AltaClienteViewModel c)
    {
        if(!ModelState.IsValid)
        {
            return RedirectToAction("ListarCliente");
        }
        Cliente cliente = new Cliente(c.Nombre,c.Email,c.Telefono);
        _clienteRepository.CrearCliente(cliente);
        return RedirectToAction("ListarCliente");
    }

    [HttpGet]
    public IActionResult ModificarCliente(ModificarClienteViewModel cliente)
    {
        return View(cliente);
    }

    [HttpPost]
    public IActionResult ModificarClientePost(ModificarClienteViewModel c)
    {
        if(!ModelState.IsValid)
        {
            return RedirectToAction("ListarCliente");
        }
        Cliente cliente = new Cliente(c.ClienteId,c.Nombre,c.Email,c.Telefono);
        _clienteRepository.modificarCliente(cliente);
        return RedirectToAction("ListarCliente");
    }

    [HttpGet]
    public IActionResult EliminarCliente(Cliente cliente)
    {
        return View(cliente);
    }

    [HttpPost]
    public IActionResult EliminarClientePost(int ClienteId)
    {
        _clienteRepository.EliminarCliente(ClienteId);
        return RedirectToAction("ListarCliente");
    }
}
