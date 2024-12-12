public interface IClienteRepository
{
    public void CrearCliente(Cliente cliente);
    public void modificarCliente(Cliente c);
    public List<Cliente> listarClientes();
    public void EliminarCliente(int id);
    public Cliente obtenerCliente(int id);
}