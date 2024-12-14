using System.ComponentModel.DataAnnotations;
public class AltaProductoViewModel
{
    public AltaProductoViewModel()
    {
    }

    [StringLength(250)]
    public string Descripcion  { get ; set;}
    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
    public int Precio { get ; set;}

}