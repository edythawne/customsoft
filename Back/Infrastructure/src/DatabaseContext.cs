using Npgsql;

namespace Infrastructure;

public class DatabaseContext {
    
    private static string? ConnectionString { get; set; }
    public static NpgsqlConnection NpgsqlContextInstance => CreateNpgsqlConnection();

    public DatabaseContext(string param) {
        ConnectionString = param;
    }
    
    /**
     * Crear un objecto de tipo NpgsqlConnection
     */
    private static NpgsqlConnection CreateNpgsqlConnection() {
        return new NpgsqlConnection(ConnectionString);
    }

}