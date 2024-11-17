
public class Producto 
{
    private int idProducto;
    private string descripcion;
    private int precio;


    public Producto(int idProducto, string descripcion, int precio)
    {
        this.idProducto = idProducto;
        this.descripcion = descripcion;
        this.precio = precio;
    }

    public Producto()
    {
        
    }

    public int Precio { get => precio; set => precio = value;}
    public string Descripcion { get => descripcion; set => descripcion = value;}
    public int IdProducto { get => idProducto; set => idProducto = value;}

}


