
public class Presupuesto
{
    private int idPresupuesto;
    //private string nombreDestinatario;

    private Cliente cliente;
    private DateTime fechaCreacion;
    private List<PresupuestoDetalle> detalle;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value;}
    //public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value;}
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value;}
    public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value;}
    public Cliente Cliente { get => cliente; set => cliente = value; }

    public Presupuesto(int idPresupuesto/*, string nombreDestinatario*/, DateTime fechaCreacion)
    {
        this.idPresupuesto = idPresupuesto;
        //this.nombreDestinatario = nombreDestinatario;
        this.fechaCreacion = fechaCreacion;
        this.detalle = new List<PresupuestoDetalle>();
    }

    public Presupuesto()
    {
        /*this.idPresupuesto = -1;
        this.nombreDestinatario = "aa";
        this.fechaCreacion = new DateTime();
        this.detalle = new List<PresupuestoDetalle>();*/
        this.fechaCreacion = DateTime.Now.Date;
    }
    public float MontoPresupuesto()
    {
        float monto = 0;
        foreach (PresupuestoDetalle pd in Detalle)
        {
            monto = monto + (pd.Producto.Precio * pd.Cantidad);
        }
        
        return monto;
    }

    public double montoPresupuestoConIva()
    {
        return (MontoPresupuesto() * 1.21);
    }

    public int cantidadProductos()
    {
        return Detalle.Count;
    }
    
    public void agregarDetalle(PresupuestoDetalle detalle)
    {
        this.detalle.Add(detalle);
    }
}