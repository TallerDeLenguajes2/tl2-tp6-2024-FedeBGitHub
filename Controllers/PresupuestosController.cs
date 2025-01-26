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
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) return RedirectToAction ("Index", "Logeo");
            List<Presupuesto> listaPresupuestos = _presupuestoRepository.ObtenerPresupuestos();
            foreach (Presupuesto p in listaPresupuestos)
            {
                p.Cliente = _clienteRepository.obtenerCliente(p.Cliente.ClienteId);
            }
            return View(listaPresupuestos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar presupuestos";
            return View(new List<Presupuesto>());
        }
    }


    
    [HttpGet]
    public IActionResult DetallePresupuesto(int id, int ClienteId)
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) return RedirectToAction ("Index", "Logeo");
            Presupuesto presupuesto = _presupuestoRepository.ObtenerDetalle(id);
            return View(presupuesto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar el detalle";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }



    [HttpGet]
    public IActionResult CrearPresupuesto()
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) {
                return RedirectToAction("Index", "Logeo");
            }
            if (HttpContext.Session.GetString("Rol") != Rol.Admin.ToString()){
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }   
            PresupuestoViewModel presupuestoViewModel = new PresupuestoViewModel();
            presupuestoViewModel.listaClientes = _clienteRepository.listarClientes();
            return View(presupuestoViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar el formulario para crear presupuesto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }



    [HttpPost]
    public IActionResult CrearPresupuestoPost(int ClienteSeleccionado, Presupuesto presupuesto)
    {
        try
        {
            presupuesto.Cliente = _clienteRepository.obtenerCliente(ClienteSeleccionado);
            _presupuestoRepository.CrearPresupuesto(presupuesto);
            return RedirectToAction("ListarPresupuesto");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo crear el presupuesto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }


    
    [HttpGet]
    public IActionResult ModificarPresupuesto(int ClienteId, Presupuesto presupuesto)
    {

        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) {
                return RedirectToAction("Index", "Logeo");
            }
            if (HttpContext.Session.GetString("Rol") != Rol.Admin.ToString()){
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }   
            presupuesto.Cliente = new Cliente(); // debo cambiar esto que el constructor de presupuesto inicialice el cliente
            presupuesto.Cliente.ClienteId = ClienteId; // no se si conviene eso o hacer que traiga el Cliente completo con el metodo del repositorio
            return View(presupuesto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar el formulario para modificar presupuesto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }



    [HttpPost]
    public IActionResult ModificarPresupuestoPost(Presupuesto presupuesto)
    {
        try
        {
            _presupuestoRepository.modificarPresupuesto(presupuesto);
            return RedirectToAction("ListarPresupuesto");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo modificar presupuesto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }

    

    [HttpGet]
    public IActionResult EliminarPresupuesto(int ClienteId, Presupuesto presupuesto)
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) {
                return RedirectToAction("Index", "Logeo");
            }
            if (HttpContext.Session.GetString("Rol") != Rol.Admin.ToString()){
                return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }  
            presupuesto.Cliente = new Cliente();
            presupuesto.Cliente.ClienteId = ClienteId;
            return View(presupuesto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar el formulario para eliminar presupuesto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }



    [HttpPost]
    public IActionResult EliminarPresupuestoPost(int IdPresupuesto)
    {
        try
        {
            _presupuestoRepository.EliminarPresupuesto(IdPresupuesto);
            return RedirectToAction("ListarPresupuesto");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo eliminar presupuesto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }



    [HttpGet]
    public IActionResult AgregarProducto(ProductosYpresupuestoViewModel vm)
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) {
                    return RedirectToAction("Index", "Logeo");
                }
            if (HttpContext.Session.GetString("Rol") != Rol.Admin.ToString()){
                    return RedirectToAction("ListarPresupuesto", "Presupuestos");
            }  
            vm.listaProductos = _productoRepository.listarProductos();
            return View(vm);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar formulario para agrgar producto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }

    

    [HttpPost]
    public IActionResult AgregarProductoPost(ProductosYpresupuestoViewModel vm)
    {
        try
        {
            _presupuestoRepository.agregarDetalle(vm.IdPresupuesto,vm.IdProducto,vm.Cantidad);
            return RedirectToAction("ListarPresupuesto");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo agregar el producto al presupuesto";
            return View("ListarPresupuesto", new List<Presupuesto>());
        }
    }


    //asp-for="NombreDestinatario" Genera name, id y value

    /* -------- SON TOTALMENTE EQUIVALENTES
    <input asp-for="FechaCreacion" class="form-control" type="date" />
    <input name="FechaCreacion" id="FechaCreacion" class="form-control" type="date" value="@Model.FechaCreacion"/>
    */
}
