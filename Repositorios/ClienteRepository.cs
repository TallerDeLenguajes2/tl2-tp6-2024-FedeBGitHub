using System.ComponentModel.DataAnnotations;
using  Microsoft.Data.Sqlite;

namespace repositorys;

public class ClienteRepository : IClienteRepository
{
    private string _cadenaConexion;

    public ClienteRepository(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    public void CrearCliente(Cliente cliente)
    {
        using ( SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "INSERT INTO Clientes (Nombre, Email, Telefono) VALUES (@Nombre, @Email, @Telefono)"; 
            // el ClienteId no por que es Auto Incremental
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@Nombre", cliente.Nombre));
            command.Parameters.Add(new SqliteParameter("@Email", cliente.Email));
            command.Parameters.Add(new SqliteParameter("@Telefono", cliente.Telefono));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void modificarCliente(Cliente c)
    {
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "UPDATE Clientes SET Nombre = @Nombre, Email = @Email, Telefono = @Telefono WHERE ClienteId = @ClienteId;";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query,connection);
            command.Parameters.Add(new SqliteParameter("@Nombre",c.Nombre));
            command.Parameters.Add(new SqliteParameter("@Email",c.Email));
            command.Parameters.Add(new SqliteParameter("@Telefono",c.Telefono));
            command.Parameters.Add(new SqliteParameter("@ClienteId",c.ClienteId));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public List<Cliente> listarClientes()
    {
        List<Cliente> LClientes = new List<Cliente>();
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "SELECT * FROM Clientes";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.ClienteId = Convert.ToInt32(reader["ClienteId"]);
                    cliente.Nombre = Convert.ToString(reader["Nombre"]);
                    cliente.Email = Convert.ToString(reader["Email"]);
                    cliente.Telefono = Convert.ToString(reader["Telefono"]);

                    LClientes.Add(cliente);
                }
            }
            connection.Close();
        }
        return LClientes;
    }

    public void EliminarCliente(int id)
    {
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "DELETE FROM Clientes WHERE ClienteId = @ClienteId;";
            connection.Open();
            SqliteCommand command = new SqliteCommand (query, connection);
            command.Parameters.Add(new SqliteParameter("@ClienteId",id));
            command.ExecuteNonQuery();
            connection.Close();
        }
    } 

    public Cliente obtenerCliente(int id)
    {
        Cliente c = null;
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "SELECT * FROM Clientes WHERE ClienteId = @id";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id",id));
            using (SqliteDataReader reader = command.ExecuteReader())
            {

            
                while (reader.Read())
                {
                    c = new Cliente();
                    c.ClienteId = Convert.ToInt32(reader["ClienteId"]);
                    c.Nombre = Convert.ToString(reader["Nombre"]);
                    c.Email = Convert.ToString(reader["Email"]);;
                    c.Telefono = Convert.ToString(reader["Telefono"]);
                }
            }
            connection.Close();
        }
        return c;
    }
}