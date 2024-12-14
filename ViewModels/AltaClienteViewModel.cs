using System.ComponentModel.DataAnnotations;
public class AltaClienteViewModel
{
    private string nombre;
    private string email;
    private string telefono;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get => nombre; set => nombre = value; }
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
    public string Email { get => email; set => email = value; }
    [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
    public string Telefono { get => telefono; set => telefono = value; }
}