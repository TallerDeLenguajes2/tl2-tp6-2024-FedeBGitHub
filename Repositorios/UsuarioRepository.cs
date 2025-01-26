using System.ComponentModel.DataAnnotations;
using  Microsoft.Data.Sqlite;

namespace repositorys;

public class UsuarioRepository: IUsuarioRepository
{
    private readonly string _cadenaConexion;
    public UsuarioRepository(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }



    public Usuario obtenerUsuario(string nomUsuario, string contrasenia)
    {
        Usuario usuario = null;
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "SELECT * FROM Usuario WHERE Usuario = @usuario AND Contrasenia = @contra";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@usuario",nomUsuario));
            command.Parameters.Add(new SqliteParameter("@contra",contrasenia));
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.IdUsuario = Convert.ToInt32(reader["idUsuario"]);
                    usuario.Nombre = reader["Nombre"].ToString();
                    usuario.NomUsuario = reader["Usuario"].ToString();
                    usuario.Contrasenia = reader["Contrasenia"].ToString();
                    usuario.Rol = (Rol)Convert.ToInt32(reader["idRol"]);
                }
            }
            connection.Close();
        }

        if (usuario == null)
        {
            throw new Exception("No se encontro el usuario");
        }

        return usuario;
    }



    public bool existeUsuario(Usuario usuario)
    {
        int totalFilas = 0;

        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            connection.Open();

            // Contar filas
            string countQuery = "SELECT COUNT(*) FROM Usuario WHERE Usuario = @nom AND Contrasenia = @contra";
            using (SqliteCommand countCommand = new SqliteCommand(countQuery, connection))
            {
                countCommand.Parameters.Add(new SqliteParameter("@nom", usuario.Nombre));
                countCommand.Parameters.Add(new SqliteParameter("@contra", usuario.Contrasenia));
                totalFilas = Convert.ToInt32(countCommand.ExecuteScalar());
            }
                connection.Close();
        }
        return totalFilas>0;
    } 



    public bool altaUsuario(Usuario usuario)
    {
        if (existeUsuario(usuario))
        {
            return false;
        }else{

            using ( SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                string query = "INSERT INTO Usuario (Nombre, Usuario, Contrasenia, idRol) VALUES (@Nombre, @usuario, @contra, @rol)"; 
                
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@Nombre", usuario.Nombre));
                command.Parameters.Add(new SqliteParameter("@usuario", usuario.NomUsuario));
                command.Parameters.Add(new SqliteParameter("@contra", usuario.Contrasenia));
                command.Parameters.Add(new SqliteParameter("@rol", (int)usuario.Rol));
                
                // Ejecuta la consulta y verifica el número de filas afectadas
                int filasAfectadas = command.ExecuteNonQuery();

                if (filasAfectadas == 0) // si no hay filas afectadas es porque no se inserto nada
                {
                    throw new Exception("No se pudo insertar el usuario en la base de datos.");
                }

                connection.Close();
            }
            return true;
        }
    }


}