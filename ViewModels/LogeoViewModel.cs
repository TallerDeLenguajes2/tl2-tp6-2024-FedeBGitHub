using System.ComponentModel.DataAnnotations;
public class LogeoViewModel
{
    private string nomUsuario;
    private string contrasenia;
    private string errorMessage;
    private bool isAuthenticated;

    public string NomUsuario { get => nomUsuario; set => nomUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
    public bool IsAuthenticated { get => isAuthenticated; set => isAuthenticated = value; }
}
