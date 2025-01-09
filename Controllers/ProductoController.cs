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

    public ProductoController(ILogger<ProductoController> logger, IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult ListarProducto()
    {
        List<Producto> listaProductos = _productoRepository.listarProductos();
        return View(listaProductos);
    }


    [HttpGet]
    public IActionResult CrearProducto()
    {
        return View(new AltaProductoViewModel());
    }


    [HttpPost]
    public IActionResult CrearProductoPost(AltaProductoViewModel producto)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarProducto");
        }
        Producto p = new Producto(producto.Descripcion,producto.Precio);
        _productoRepository.CrearProducto(p);
        return RedirectToAction("ListarProducto"); 
    }


    [HttpGet]
    public IActionResult ModificarProducto(ModificarProductoViewModel producto)
    {
        return View(producto);
    }


    [HttpPost]
    public IActionResult ModificarProductoPost(ModificarProductoViewModel p)
    {
         if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarProducto");
        }
        Producto producto = new Producto(p.IdProducto, p.Descripcion, p.Precio);
        _productoRepository.modificarProducto( producto.IdProducto, producto);
        return RedirectToAction("ListarProducto");
    }


    [HttpGet]
    public IActionResult EliminarProducto(Producto producto)
    {
        return View(producto);
    }


    [HttpPost]
    public IActionResult EliminarProductoPost(int IdProducto)
    {
        _productoRepository.EliminarProducto(IdProducto);
        return RedirectToAction("ListarProducto");
    }
    
}
