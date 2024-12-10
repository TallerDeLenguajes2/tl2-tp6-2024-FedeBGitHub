//using models;

using  Microsoft.Data.Sqlite;

namespace repositorys;

public class PresupuestosRepository
{
    const string cadenaConexion = @"Data Source=db/Tienda.db;Cache=Shared";

    public void CrearPresupuesto(Presupuesto presupuesto)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "INSERT INTO Presupuestos (NombreDestinatario,FechaCreacion) VALUES (@nombre,@fecha)";
            connection.Open();
            SqliteCommand command = new SqliteCommand (query, connection);
            command.Parameters.Add(new SqliteParameter("@nombre",presupuesto.NombreDestinatario));
            command.Parameters.Add(new SqliteParameter("@fecha",presupuesto.FechaCreacion));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public List<Presupuesto> ObtenerPresupuestos()
    {
        List<Presupuesto> p = new List<Presupuesto>();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM presupuestos";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    int id = Convert.ToInt32(reader["idPresupuesto"]);
                    //string nombre = Convert.ToString(reader["NombreDestinatario"]);
                    DateTime fecha = Convert.ToDateTime(reader["FechaCreacion"]);
                    p.Add(new Presupuesto(id, "sdsd", fecha));
                }
            }
            connection.Close();
        }
        return p;
    }

    public Presupuesto ObtenerDetalle(int idBuscado)
    {
        // Traigo un presupuesto donde su idPresupuesto=idBuscado
        Presupuesto presupuesto = null;
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM presupuestos WHERE idPresupuesto = @id";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id",idBuscado));

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    int id = Convert.ToInt32(reader["idPresupuesto"]);
                    string nombre = Convert.ToString(reader["NombreDestinatario"]);
                    DateTime fecha = Convert.ToDateTime(reader["FechaCreacion"]);
                    presupuesto = new Presupuesto(id, nombre, fecha);
                }
            }
            connection.Close();
        }
        // Traigo todos los detalles asociados al idPresupuesto que use arriba
        ProductoRepository repoPro = new ProductoRepository();
        
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM PresupuestosDetalle WHERE idpresupuesto = @id";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id",idBuscado));

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    //int idPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    int idProd = Convert.ToInt32(reader["idProducto"]);
                    int cantidad = Convert.ToInt32(reader["Cantidad"]);
                    presupuesto.Detalle.Add(new PresupuestoDetalle(repoPro.productoPorId(idProd),cantidad));
                }
            }
            connection.Close();
        }

        return presupuesto;
    }

    public Presupuesto agregarDetalle(int idPresupuesto, int idProducto, int cantidad)
    {
        Presupuesto presupuesto = ObtenerDetalle(idPresupuesto);
        ProductoRepository repoPro = new ProductoRepository();
        Producto producto = repoPro.productoPorId(idProducto);
        if(presupuesto != null && producto!= null)
        {
            presupuesto.Detalle.Add(new PresupuestoDetalle(producto,cantidad));
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                string query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto,@idProducto,@cantidad)";
                connection.Open();
                SqliteCommand command = new SqliteCommand (query, connection);
                command.Parameters.Add(new SqliteParameter("@idPresupuesto",idPresupuesto));
                command.Parameters.Add(new SqliteParameter("@idProducto",idProducto));
                command.Parameters.Add(new SqliteParameter("@cantidad",cantidad));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        if(presupuesto == null || producto == null)
        {
            return null;
        }
        return presupuesto;
    }

    public void modificarPresupuesto(Presupuesto presupuesto)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "UPDATE Presupuestos SET NombreDestinatario = @nom , FechaCreacion = @fechCreacion WHERE idPresupuesto = @idPresupuesto;";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query,connection);
            command.Parameters.Add(new SqliteParameter("@nom",presupuesto.NombreDestinatario));
            command.Parameters.Add(new SqliteParameter("@fechCreacion",presupuesto.FechaCreacion));
            command.Parameters.Add(new SqliteParameter("@idPresupuesto",presupuesto.IdPresupuesto));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void EliminarPresupuesto(int idPresupuesto)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "DELETE FROM Presupuestos WHERE idPresupuesto = @idPresupuesto";
            connection.Open();
            SqliteCommand command = new SqliteCommand (query, connection);
            command.Parameters.Add(new SqliteParameter("@idPresupuesto",idPresupuesto));
            command.ExecuteNonQuery();
            connection.Close();
        }
    } 
}

