
public class Presupuesto
{
    private int idPresupuesto;
    private Cliente cliente;
    private DateTime fechaCreacion;
    private List<PresupuestoDetalle> detalle;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value;}
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value;}
    public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value;}
    

    public Presupuesto(int idPresupuesto, DateTime fechaCreacion)
    {
        this.idPresupuesto = idPresupuesto;
        this.fechaCreacion = fechaCreacion;
        this.detalle = new List<PresupuestoDetalle>();
    }

    public Presupuesto()
    {
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