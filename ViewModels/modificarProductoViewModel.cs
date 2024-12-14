using System.ComponentModel.DataAnnotations;
public class ModificarProductoViewModel
{
    public ModificarProductoViewModel()
    {
    }

    public int IdProducto { get ; set;}
    [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
    public string Descripcion  { get ; set;}
    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
    public int Precio { get ; set;}

}