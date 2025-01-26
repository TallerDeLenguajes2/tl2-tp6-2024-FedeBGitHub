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
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) return RedirectToAction ("Index", "Logeo");
            List<Producto> listaProductos = _productoRepository.listarProductos();
            return View(listaProductos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar productos";
            return View(new List<Presupuesto>());
        }
    }


    [HttpGet]
    public IActionResult CrearProducto()
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) {
                return RedirectToAction("Index", "Logeo");
            }
            if (HttpContext.Session.GetString("Rol") != Rol.Admin.ToString()){
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }   
            return View(new AltaProductoViewModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar el formulario para crear productos";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }


    [HttpPost]
    public IActionResult CrearProductoPost(AltaProductoViewModel producto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListarProducto");
            }
            Producto p = new Producto(producto.Descripcion,producto.Precio);
            _productoRepository.CrearProducto(p);
            return RedirectToAction("ListarProducto"); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo crear el producto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }


    [HttpGet]
    public IActionResult ModificarProducto(ModificarProductoViewModel producto)
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) {
                return RedirectToAction("Index", "Logeo");
            }
            if (HttpContext.Session.GetString("Rol") != Rol.Admin.ToString()){
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }   
            return View(producto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar el formulario para modificar producto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }


    [HttpPost]
    public IActionResult ModificarProductoPost(ModificarProductoViewModel p)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListarProducto");
            }
            Producto producto = new Producto(p.IdProducto, p.Descripcion, p.Precio);
            _productoRepository.modificarProducto( producto.IdProducto, producto);
            return RedirectToAction("ListarProducto");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo modificar el producto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }


    [HttpGet]
    public IActionResult EliminarProducto(Producto producto)
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) {
                return RedirectToAction("Index", "Logeo");
            }
            if (HttpContext.Session.GetString("Rol") != Rol.Admin.ToString()){
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }   
            return View(producto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar el formulario para modificar producto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }


    [HttpPost]
    public IActionResult EliminarProductoPost(int IdProducto)
    {
        try
        {
            _productoRepository.EliminarProducto(IdProducto);
            return RedirectToAction("ListarProducto");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al eliminar el producto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }
    
}
