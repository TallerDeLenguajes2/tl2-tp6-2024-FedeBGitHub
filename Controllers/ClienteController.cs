using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_FedeBGitHub.Models;
using repositorys;

//namespace models.Controllers;

namespace tl2_tp6_2024_FedeBGitHub.Controllers;

public class ClienteController : Controller
{
    private  ClienteRepository clienteRepository;
    private readonly ILogger<ClienteController> _logger;

    public ClienteController(ILogger<ClienteController> logger)
    {
        clienteRepository = new ClienteRepository();
        _logger = logger;
    }

    
    [HttpGet]
    public IActionResult ListarCliente()
    {
        List<Cliente> listaClientes = clienteRepository.listarClientes();
        return View(listaClientes);
    }

    [HttpGet]
    public IActionResult CrearClienteFormulario()
    {
        return View(new Cliente());
    }

    [HttpPost]
    public IActionResult CrearCliente(Cliente cliente)
    {
        clienteRepository.CrearCliente(cliente);
        return RedirectToAction("ListarCliente");
    }

    [HttpGet]
    public IActionResult ModificarClienteFormulario(Cliente cliente)
    {
        return View(cliente);
    }

    [HttpPost]
    public IActionResult ModificarCliente(Cliente cliente)
    {
        clienteRepository.modificarCliente(cliente);
        return RedirectToAction("ListarCliente");
    }

    [HttpGet]
    public IActionResult EliminarConfirmacion(Cliente cliente)
    {
        return View(cliente);
    }

    [HttpPost]
    public IActionResult EliminarCliente(int ClienteId)
    {
        clienteRepository.EliminarCliente(ClienteId);
        return RedirectToAction("ListarCliente");
    }
    /*
   

    [HttpPost]
    public IActionResult AddProductoForm(ProductosYpresupuestoViewModel vm)
    {
        ProductoRepository productoRepository = new ProductoRepository();
        vm.listaProductos = productoRepository.listarProductos();
        return View(vm);
    }

    
    [HttpPost]
    public IActionResult AddProducto(ProductosYpresupuestoViewModel vm)
    {
        presupuestoRepository.agregarDetalle(vm.IdPresupuesto,vm.IdProducto,vm.Cantidad);
        return RedirectToAction("Listar_Presupuesto");
    }
    */

    //asp-for="NombreDestinatario" Genera name, id y value

    /* -------- SON TOTALMENTE EQUIVALENTES
    <input asp-for="FechaCreacion" class="form-control" type="date" />
    <input name="FechaCreacion" id="FechaCreacion" class="form-control" type="date" value="@Model.FechaCreacion"/>
    */
}
