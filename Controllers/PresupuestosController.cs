using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_FedeBGitHub.Models;
using repositorys;

//namespace models.Controllers;

namespace tl2_tp6_2024_FedeBGitHub.Controllers;

public class PresupuestosController : Controller
{
    private  IPresupuestosRepository _presupuestoRepository;
    private IProductoRepository _productoRepository;
    private  IClienteRepository _clienteRepository;
    private readonly ILogger<ProductoController> _logger;

    public PresupuestosController(ILogger<ProductoController> logger, IPresupuestosRepository presupuestoRepository, IProductoRepository productoRepository, IClienteRepository clienteRepository)
    {
        _presupuestoRepository = presupuestoRepository;
        _clienteRepository = clienteRepository;
        _productoRepository = productoRepository;
        _logger = logger;
    }



    [HttpGet]
    public IActionResult ListarPresupuesto()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) return RedirectToAction ("Index", "Logeo");
        List<Presupuesto> listaPresupuestos = _presupuestoRepository.ObtenerPresupuestos();
        foreach (Presupuesto p in listaPresupuestos)
        {
            p.Cliente = _clienteRepository.obtenerCliente(p.Cliente.ClienteId);
        }
        return View(listaPresupuestos);
    }


    
    [HttpGet]
    public IActionResult DetallePresupuesto(int id, int ClienteId)
    {
        
        Presupuesto presupuesto = _presupuestoRepository.ObtenerDetalle(id);
        return View(presupuesto);
    }



    [HttpGet]
    public IActionResult CrearPresupuesto()
    {
        PresupuestoViewModel presupuestoViewModel = new PresupuestoViewModel();
        presupuestoViewModel.listaClientes = _clienteRepository.listarClientes();
        return View(presupuestoViewModel);
    }



    [HttpPost]
    public IActionResult CrearPresupuestoPost(int ClienteSeleccionado, Presupuesto presupuesto)
    {
        presupuesto.Cliente = _clienteRepository.obtenerCliente(ClienteSeleccionado);
        _presupuestoRepository.CrearPresupuesto(presupuesto);
        return RedirectToAction("ListarPresupuesto");
    }


    
    [HttpGet]
    public IActionResult ModificarPresupuesto(int ClienteId, Presupuesto presupuesto)
    {
        presupuesto.Cliente = new Cliente(); // debo cambiar esto que el constructor de presupuesto inicialice el cliente
        presupuesto.Cliente.ClienteId = ClienteId; // no se si conviene eso o hacer que traiga el Cliente completo con el metodo del repositorio
        return View(presupuesto);
    }



    [HttpPost]
    public IActionResult ModificarPresupuestoPost(Presupuesto presupuesto)
    {
        _presupuestoRepository.modificarPresupuesto(presupuesto);
        return RedirectToAction("ListarPresupuesto");
    }

    

    [HttpGet]
    public IActionResult EliminarPresupuesto(int ClienteId, Presupuesto presupuesto)
    {
        presupuesto.Cliente = new Cliente();
        presupuesto.Cliente.ClienteId = ClienteId;
        return View(presupuesto);
    }



    [HttpPost]
    public IActionResult EliminarPresupuestoPost(int IdPresupuesto)
    {
        _presupuestoRepository.EliminarPresupuesto(IdPresupuesto);
        return RedirectToAction("ListarPresupuesto");
    }



    [HttpGet]
    public IActionResult AgregarProducto(ProductosYpresupuestoViewModel vm)
    {
        vm.listaProductos = _productoRepository.listarProductos();
        return View(vm);
    }

    

    [HttpPost]
    public IActionResult AgregarProductoPost(ProductosYpresupuestoViewModel vm)
    {
        _presupuestoRepository.agregarDetalle(vm.IdPresupuesto,vm.IdProducto,vm.Cantidad);
        return RedirectToAction("ListarPresupuesto");
    }


    //asp-for="NombreDestinatario" Genera name, id y value

    /* -------- SON TOTALMENTE EQUIVALENTES
    <input asp-for="FechaCreacion" class="form-control" type="date" />
    <input name="FechaCreacion" id="FechaCreacion" class="form-control" type="date" value="@Model.FechaCreacion"/>
    */
}
