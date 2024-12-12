public interface IPresupuestosRepository
{
    public void CrearPresupuesto(Presupuesto presupuesto);
    public List<Presupuesto> ObtenerPresupuestos();
    public Presupuesto ObtenerDetalle(int idBuscado);
    public Presupuesto agregarDetalle(int idPresupuesto, int idProducto, int cantidad);
    public void modificarPresupuesto(Presupuesto presupuesto);
    public void EliminarPresupuesto(int idPresupuesto);
}