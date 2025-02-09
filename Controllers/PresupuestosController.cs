using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_FedeBGitHub.Models;
using repositorys;

//namespace models.Controllers;

namespace tl2_tp6_2024_FedeBGitHub.Controllers;

public class PresupuestosController : Controller
{
    private  PresupuestosRepository presupuestoRepository;
    private readonly ILogger<ProductoController> _logger;

    public PresupuestosController(ILogger<ProductoController> logger)
    {
        presupuestoRepository = new PresupuestosRepository();
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    
    [HttpGet]
    public IActionResult Listar_Presupuesto()
    {
        List<Presupuesto> listaPresupuestos = presupuestoRepository.ObtenerPresupuestos();
        return View(listaPresupuestos);
    }

    
    [HttpPost]
    public IActionResult ObtenerDetalle(int id)
    {
        Presupuesto presupuesto = presupuestoRepository.ObtenerDetalle(id);
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult CrearPresupuestoFormulario()
    {
        return View(new Presupuesto());
    }

    [HttpPost]
    public IActionResult CrearPresupuesto(Presupuesto presupuesto)
    {
        presupuestoRepository.CrearPresupuesto(presupuesto);
        return RedirectToAction("Listar_Presupuesto");
    }

    [HttpPost]
    public IActionResult ModificarPresupuestoForm(Presupuesto presupuesto)
    {
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult ModificarPresupuesto(Presupuesto presupuesto)
    {
        presupuestoRepository.modificarPresupuesto(presupuesto);
        return RedirectToAction("Listar_Presupuesto");
    }

    [HttpPost]
    public IActionResult EliminarPresupuestoPag(Presupuesto presupuesto)
    {
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult EliminarPresupuesto(int IdPresupuesto)
    {
        presupuestoRepository.EliminarPresupuesto(IdPresupuesto);
        return RedirectToAction("Listar_Presupuesto");
    }

    [HttpGet]
    public IActionResult VolverAInicio()
    {
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
