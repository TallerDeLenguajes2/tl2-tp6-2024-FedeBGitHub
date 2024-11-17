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

    public IActionResult Index()
    {
        return View();
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
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarProducto()
    {
        return View(new Producto());
    }
    [HttpPost]
    public IActionResult ModificarProducto(Producto Producto)
    {
        productoRepository.modificarProducto( Producto.IdProducto, Producto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EliminarProducto(int id)
    {
        Producto producto = productoRepository.productoPorId(id);
        return View(producto);
    }
    [HttpPost]
    public IActionResult EliminarMetodo(Producto producto)
    {
        productoRepository.EliminarProducto(producto.IdProducto);
        return RedirectToAction("Index");
    }
}
