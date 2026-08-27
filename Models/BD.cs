using Microsoft.Data.SqlClient;
using Dapper;
namespace TP06PoniachikDanaFalk.Models;
public class BD
{
    
    private string _connectionString = @"Server=localhost;Database=BDTP06;Integrated Security=True;TrustServerCertificate=True";

    public int crearUsuario(string nombre) {

        string query = @"INSERT INTO Usuarios (NombreUsuario) VALUES (@pNombre); SELECT CAST(SCOPE_IDENTITY() AS int);";
        using var connection = new SqlConnection(_connectionString);
        return connection.ExecuteScalar<int>(query, new { pNombre = nombre });
    
    }

}
