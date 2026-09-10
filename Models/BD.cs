using Microsoft.Data.SqlClient;
using Dapper;

namespace TP06PoniachikDanaFalk.Models;

public class BD
{
    private string _connectionString = @"Server=localhost;Database=BDTP06;Integrated Security=True;TrustServerCertificate=True";

    public int crearUsuario(string nombre)
    {
        string query = "INSERT INTO Usuarios (NombreUsuario) VALUES (@pNombre);";
        using SqlConnection connection = new SqlConnection(_connectionString);
        return connection.Execute(query, new { @pNombre = nombre });
    }

    public int ultimoUsuario()
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        string query = "SELECT TOP 1 ID FROM Usuarios ORDER BY ID DESC";
        return connection.QueryFirstOrDefault<int>(query);
    }

    public string palabraAhorcado(int numRandom)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        string query = "SELECT nombre FROM PalabrasAhorcado WHERE ID = @pID";
        return connection.QueryFirstOrDefault<string>(query, new { pID = numRandom });
    }

    public List<PalabrasRosco> palabrasRosco()
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        string query = "SELECT * FROM PalabrasRosco";
        return connection.Query<PalabrasRosco>(query).ToList();
    }

    public List<Adivinanzas> adivinanzas()
    {
        try
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT ID, adivinanza, respuesta FROM adivinanza ORDER BY ID";
            return connection.Query<Adivinanzas>(query).ToList();
        }
        catch (SqlException ex) when (ex.Number == 208)
        {
            return new List<Adivinanzas>();
        }
    }
}
