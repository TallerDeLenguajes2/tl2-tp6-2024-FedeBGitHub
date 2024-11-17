
public class PresupuestoDetalle
{
    private Producto producto;
    private int cantidad;

    
    public int Cantidad { get => cantidad;}
    public Producto Producto { get => producto; }

    public PresupuestoDetalle(Producto producto, int cantidad)
    {
        this.producto = producto;
        this.cantidad = cantidad;
    }
    void cargarProducto(Producto p)
    {
        this.producto = p;
    }
}