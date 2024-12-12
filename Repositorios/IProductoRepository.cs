public interface IProductoRepository
{
    public void CrearProducto(Producto producto);
    public void modificarProducto(int id, Producto prod);
    public List<Producto> listarProductos();
    public Producto productoPorId(int idBuscado);
    public void EliminarProducto(int idProducto);
}