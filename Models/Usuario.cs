using Microsoft.AspNetCore.Identity;

public class Usuario
{
    private int idUsuario;
    private string nombre;
    private string nomUsuario;
    private string contrasenia;
    private Rol rol;

    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string NomUsuario { get => nomUsuario; set => nomUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public Rol Rol { get => rol; set => rol = value; }
}

public enum Rol
{
    Admin,
    Cliente
}

