//using models;

using  Microsoft.Data.Sqlite;

namespace repositorys;

public class PresupuestosRepository: IPresupuestosRepository
{
    private string _cadenaConexion;
    IProductoRepository _productoRepository;


    public PresupuestosRepository(string cadenaConexion, IProductoRepository productoRepository)
    {
        _cadenaConexion = cadenaConexion;
        _productoRepository = productoRepository;
    }


    public void CrearPresupuesto(Presupuesto presupuesto)
    {
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "INSERT INTO Presupuestos (ClienteID,FechaCreacion) VALUES (@clienteId,@fecha)";
            connection.Open();
            SqliteCommand command = new SqliteCommand (query, connection);
            command.Parameters.Add(new SqliteParameter("@clienteId",presupuesto.Cliente.ClienteId));
            command.Parameters.Add(new SqliteParameter("@fecha",presupuesto.FechaCreacion.ToString("yyyy-MM-dd")));
           
            int filasAfectadas = command.ExecuteNonQuery();

            if (filasAfectadas == 0) // si no hay filas afectadas es porque no se inserto nada
            {
                throw new Exception("No se pudo insertar el presupuesto en la base de datos.");
            }
            connection.Close();
        }
    }



    public List<Presupuesto> ObtenerPresupuestos()
    {
        List<Presupuesto> p = new List<Presupuesto>();
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "SELECT * FROM presupuestos";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    int id = Convert.ToInt32(reader["idPresupuesto"]);
                    int ClienteId = Convert.ToInt32(reader["ClienteID"]);
                    DateTime fecha = Convert.ToDateTime(reader["FechaCreacion"]);
                    
                    Presupuesto nuevoPresupuesto = new Presupuesto();
                    nuevoPresupuesto.Cliente = new Cliente();
                    nuevoPresupuesto.IdPresupuesto = id;
                    nuevoPresupuesto.FechaCreacion = fecha.Date;
                    nuevoPresupuesto.Cliente.ClienteId = ClienteId;
                    p.Add(nuevoPresupuesto);
                }
            }
            connection.Close();
        }

        if (p.Count == 0)
        {
            throw new Exception("No se encontro presupuestos");
        }

        return p;
    }



    //aqui lo uso
    public Presupuesto ObtenerDetalle(int idBuscado)
    {
        // Traigo un presupuesto donde su idPresupuesto=idBuscado
        Presupuesto presupuesto = null;
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "SELECT * FROM presupuestos WHERE idPresupuesto = @id";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id",idBuscado));

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    presupuesto = new Presupuesto();
                    presupuesto.Detalle = new List<PresupuestoDetalle>();
                    presupuesto.Cliente = new Cliente();
                    presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    presupuesto.Cliente.ClienteId = Convert.ToInt32(reader["ClienteId"]);
                    presupuesto.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
                }
            }
            connection.Close();
        }
        // Traigo todos los detalles asociados al idPresupuesto que use arriba

        
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
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
                    presupuesto.Detalle.Add(new PresupuestoDetalle(_productoRepository.productoPorId(idProd),cantidad));
                }
            }
            connection.Close();
        }
        return presupuesto;
    }



    // aqui lo uso
    public Presupuesto agregarDetalle(int idPresupuesto, int idProducto, int cantidad)
    {
        Presupuesto presupuesto = ObtenerDetalle(idPresupuesto);
        
        Producto producto = _productoRepository.productoPorId(idProducto);
        if(presupuesto != null && producto!= null)
        {
            presupuesto.Detalle.Add(new PresupuestoDetalle(producto,cantidad));
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
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
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "UPDATE Presupuestos SET ClienteID = @clienteId , FechaCreacion = @fechCreacion WHERE idPresupuesto = @idPresupuesto;";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query,connection);
            command.Parameters.Add(new SqliteParameter("@clienteId",presupuesto.Cliente.ClienteId));
            command.Parameters.Add(new SqliteParameter("@fechCreacion",presupuesto.FechaCreacion));
            command.Parameters.Add(new SqliteParameter("@idPresupuesto",presupuesto.IdPresupuesto));
            
            int filasAfectadas = command.ExecuteNonQuery();

            if (filasAfectadas == 0) // si no hay filas afectadas es porque no se modificó
            {
                throw new Exception("No se pudo modificar el presupuesto en la base de datos.");
            }
            connection.Close();
        }
    }



    public void EliminarPresupuesto(int idPresupuesto)
    {
        using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
        {
            string query = "DELETE FROM Presupuestos WHERE idPresupuesto = @idPresupuesto";
            connection.Open();
            SqliteCommand command = new SqliteCommand (query, connection);
            command.Parameters.Add(new SqliteParameter("@idPresupuesto",idPresupuesto));

            int filasAfectadas = command.ExecuteNonQuery();

            if (filasAfectadas == 0) // si no hay filas afectadas es porque no se eliminó
            {
                throw new Exception("No se pudo eliminar el presupuesto en la base de datos.");
            }

            connection.Close();
        }
    } 


}

