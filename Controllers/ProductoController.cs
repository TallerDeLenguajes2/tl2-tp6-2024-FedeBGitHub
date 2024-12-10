using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_FedeBGitHub.Models;
using repositorys;

//namespace models.Controllers;

namespace tl2_tp6_2024_FedeBGitHub.Controllers;

public class ProductoController : Controller
{
    private  ProductoRepository productoRepository;
    private readonly ILogger<ProductoController> _logger;

    public ProductoController(ILogger<ProductoController> logger)
    {
        productoRepository = new ProductoRepository();
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        List<Producto> listaProductos = productoRepository.listarProductos();
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
        productoRepository.CrearProducto(Producto);
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
        productoRepository.modificarProducto( Producto.IdProducto, Producto);
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
        productoRepository.EliminarProducto(IdProducto);
        return RedirectToAction("Listar");
    }
}
