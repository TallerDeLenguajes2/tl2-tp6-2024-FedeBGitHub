using  Microsoft.Data.Sqlite;

namespace repositorys;

public class ProductoRepository : IProductoRepository
{
    private readonly string _cadenaConexion; // si le borro el Cache=Shared funca igual

    public ProductoRepository(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    public void CrearProducto(Producto producto)
    {
        using ( SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }



    public void modificarProducto(int id, Producto prod)
    {
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @idProd;";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query,connection);
            command.Parameters.Add(new SqliteParameter("@Descripcion",prod.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio",prod.Precio));
            command.Parameters.Add(new SqliteParameter("@idProd",id));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }



    public List<Producto> listarProductos()
    {
        List<Producto> LProductos = new List<Producto>();
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "SELECT * FROM Productos";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["idProducto"]);
                    string descripcion = Convert.ToString(reader["Descripcion"]);
                    int precio;
                    int.TryParse(reader["Precio"].ToString(), out precio); //otra forma de conversion de datos usando TryParse funciona tambien para float y es lo que usaba en TallerI
                    LProductos.Add(new Producto (id,descripcion,precio));
                }
            }
            connection.Close();
        }
        return LProductos;
    }



    public Producto productoPorId(int idBuscado)
    {
        Producto p = null;
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
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



    public void EliminarProducto(int idProducto)
    {
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "DELETE FROM Productos WHERE idProducto = @idProducto;";
            connection.Open();
            SqliteCommand command = new SqliteCommand (query, connection);
            command.Parameters.Add(new SqliteParameter("@idProducto",idProducto));
            command.ExecuteNonQuery();
            connection.Close();
        }
    } 

}