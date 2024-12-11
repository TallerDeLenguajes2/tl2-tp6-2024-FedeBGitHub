using System.ComponentModel.DataAnnotations;
using  Microsoft.Data.Sqlite;

namespace repositorys;

public class ClienteRepository
{
    const string cadenaConexion = @"Data Source=db/Tienda.db;Cache=Shared"; // si le borro el Cache=Shared funca igual

    public void CrearCliente(Cliente cliente)
    {
        using ( SqliteConnection connection = new SqliteConnection(cadenaConexion))
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
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
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
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
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

    public Producto productoPorId(int idBuscado)
    {
        Producto p = null;
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM Productos WHERE idProducto = @id";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id",idBuscado));
            using (SqliteDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["idProducto"]);
                    string descripcion = Convert.ToString(reader["Descripcion"]);
                    int precio;
                    int.TryParse(reader["Precio"].ToString(), out precio); //otra forma de conversion de datos usando TryParse funciona tambien para float y es lo que usaba en TallerI
                    p = new Producto (id,descripcion,precio);
                }
            }
            connection.Close();
        }
        return p;
    }

    public void EliminarCliente(int id)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
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
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
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