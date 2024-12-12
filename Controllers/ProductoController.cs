using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_FedeBGitHub.Models;
using repositorys;

//namespace models.Controllers;

namespace tl2_tp6_2024_FedeBGitHub.Controllers;

public class ProductoController : Controller
{
    private  IProductoRepository _productoRepository;
    private readonly ILogger<ProductoController> _logger;

    public ProductoController(ILogger<ProductoController> logger)
    {
        _productoRepository = new ProductoRepository(@"Data Source=db/Tienda.db;Cache=Shared");
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        List<Producto> listaProductos = _productoRepository.listarProductos();
        return View(listaProductos);
    }


    [HttpGet]
    public IActionResult CrearProducto()
    {
        return View(new Producto());
    }

    [HttpPost]
    public IActionResult CrearProducto(Producto Producto)
    {
        _productoRepository.CrearProducto(Producto);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult ModificarProductoFormulario(Producto producto)
    {
        return View(new Producto());
    }
    [HttpPost]
    public IActionResult ModificarProducto(Producto Producto)
    {
        _productoRepository.modificarProducto( Producto.IdProducto, Producto);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult EliminarConfirmacion(Producto producto)
    {
        return View(producto);
    }

    [HttpPost]
    public IActionResult EliminarProducto(int IdProducto)
    {
        _productoRepository.EliminarProducto(IdProducto);
        return RedirectToAction("Listar");
    }
    
}
