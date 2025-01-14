public interface IUsuarioRepository
{
    public Usuario obtenerUsuario(string nomUsuario, string contrasenia);
    public bool existeUsuario(Usuario usuario);
    public bool altaUsuario(Usuario usuario);
}