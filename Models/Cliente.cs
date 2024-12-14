public class Cliente
{
    private int clienteId;
    private string nombre;
    private string email;
    private string telefono;

    public Cliente()
    {
    }
    // constructor para ModificarClienteViewModel
    public Cliente(int id,string nom, string email, string telefono)
    {
        this.clienteId = id;
        this.nombre = nom;
        this.email = email;
        this.telefono = telefono;
    }
    // constructor para AltaClienteViewModel
    public Cliente(string nom, string email, string telefono)
    {
        this.nombre = nom;
        this.email = email;
        this.telefono = telefono;
    }

    public int ClienteId { get => clienteId; set => clienteId = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Email { get => email; set => email = value; }
    public string Telefono { get => telefono; set => telefono = value; }
}