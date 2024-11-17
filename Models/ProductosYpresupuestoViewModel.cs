public class ProductosYpresupuestoViewModel
{
    public int IdPresupuesto { get; set; }
    public string NombreDestinatario { get; set; }
    public DateTime FechaCreacion { get; set; }
            
    public int IdProducto { get; set; }

    public int Cantidad { get; set; }
    public List<Producto> listaProductos { get; set; }
    public ProductosYpresupuestoViewModel()
    {
        
    }
}