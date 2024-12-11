using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_FedeBGitHub.Models;
using repositorys;

//namespace models.Controllers;

namespace tl2_tp6_2024_FedeBGitHub.Controllers;

public class PresupuestosController : Controller
{
    private  PresupuestosRepository presupuestoRepository;
    private  ClienteRepository clienteRepository;
    private readonly ILogger<ProductoController> _logger;

    public PresupuestosController(ILogger<ProductoController> logger)
    {
        presupuestoRepository = new PresupuestosRepository();
        clienteRepository = new ClienteRepository();
        _logger = logger;
    }

    
    [HttpGet]
    public IActionResult Listar_Presupuesto()
    {
        List<Presupuesto> listaPresupuestos = presupuestoRepository.ObtenerPresupuestos();
        foreach (Presupuesto p in listaPresupuestos)
        {
            p.Cliente = clienteRepository.obtenerCliente(p.Cliente.ClienteId);
        }
        return View(listaPresupuestos);
    }

    
    [HttpPost]
    public IActionResult ObtenerDetalle(int id, int ClienteId)
    {
        
        Presupuesto presupuesto = presupuestoRepository.ObtenerDetalle(id);
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult CrearPresupuestoFormulario()
    {
        PresupuestoViewModel presupuestoViewModel = new PresupuestoViewModel();
        presupuestoViewModel.listaClientes = clienteRepository.listarClientes();
        return View(presupuestoViewModel);
    }

    [HttpPost]
    public IActionResult CrearPresupuesto(int ClienteSeleccionado, Presupuesto presupuesto)
    {
        presupuesto.Cliente = clienteRepository.obtenerCliente(ClienteSeleccionado);
        presupuestoRepository.CrearPresupuesto(presupuesto);
        return RedirectToAction("Listar_Presupuesto");
    }

    
    [HttpPost]
    public IActionResult ModificarPresupuestoForm(int ClienteId, Presupuesto presupuesto)
    {
        presupuesto.Cliente = new Cliente(); // debo cambiar esto que el constructor de presupuesto inicialice el cliente
        presupuesto.Cliente.ClienteId = ClienteId; // no se si conviene eso o hacer que traiga el Cliente completo con el metodo del repositorio
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult ModificarPresupuesto(Presupuesto presupuesto)
    {
        presupuestoRepository.modificarPresupuesto(presupuesto);
        return RedirectToAction("Listar_Presupuesto");
    }

    
    [HttpPost]
    public IActionResult EliminarPresupuestoPag(int ClienteId, Presupuesto presupuesto)
    {
        presupuesto.Cliente = new Cliente();
        presupuesto.Cliente.ClienteId = ClienteId;
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult EliminarPresupuesto(int IdPresupuesto)
    {
        presupuestoRepository.EliminarPresupuesto(IdPresupuesto);
        return RedirectToAction("Listar_Presupuesto");
    }

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


    //asp-for="NombreDestinatario" Genera name, id y value

    /* -------- SON TOTALMENTE EQUIVALENTES
    <input asp-for="FechaCreacion" class="form-control" type="date" />
    <input name="FechaCreacion" id="FechaCreacion" class="form-control" type="date" value="@Model.FechaCreacion"/>
    */
}
