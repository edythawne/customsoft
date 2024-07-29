using System.Data;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;

namespace Infrastructure.Repository.Common;

public abstract class BaseRepository {
    private const string Tag = "BaseRepository";
    private readonly NpgsqlConnection _connection;
    private Dictionary<string, object>? _dictionary;

    /**
     * Constructor
     */
    protected BaseRepository() {
        _connection = DatabaseContext.NpgsqlContextInstance;
    }

    /**
     * Permite agregar los parametros para los procedimientos
     * <param name="dictionary"></param>
     */
    public void SetParameters(Dictionary<string, object> dictionary) {
        _dictionary = dictionary;
    }

    /**
     * Obtiene el diccionario de datos
     */
    private Dictionary<string, object>? GetParameters() {
        return _dictionary;
    }

    /**
     * Permite abrir una conexion
     */
    protected async Task OpenConnection() {
        if (_connection.State != ConnectionState.Open) {
            await _connection.OpenAsync();
        }
    }

    /**
     * Permite crear una conexion
     */
    protected void CloseConnection() {
        _connection.Close();
    }

    /**
     * Permite preparar la sentencia SQL y los parametros que requerira para realizar la consulta a la base de datos
     * <param name="parameters">Diccionario de Parametros</param>
     * <param name="isStoreProcedure">El actual query es una funcion o procedimiento almacenamiento</param>
     * <param name="query">SQL Statement</param>
     */
    private NpgsqlCommand ApplyParameters(bool isStoreProcedure, string query, Dictionary<string, object>? parameters) {
        var command = _connection.CreateCommand();
        command.CommandText = query;
        command.CommandType = isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text;

        if (parameters != null) {
            foreach (var parameter in parameters) {
                command.Parameters.Add(new NpgsqlParameter(parameter.Key, parameter.Value));
            }   
        }

        return command;
    }
    
    /**
     * Permite convertir el json que regresan los procedimientos almacenados a una clase pojo
     * <param name="query">Nombre del procedimiento</param>
     */
    protected async Task<T?> ApplyProcedure<T>(string query) {
        try {
            string? json = null;
            var reader = ApplyParameters(true, query, GetParameters()).ExecuteReader();
            
            await using (reader) {
                if (reader.HasRows) {
                    while (reader.Read()) {
                        json = reader.GetValue(0).ToString();
                    }
                }
            }
            
            if (json != null) {
                return JsonConvert.DeserializeObject<T>(json);
            }

            return default(T?);
            
        } catch (NpgsqlException exception) {
            throw new System.Exception($"{exception.Message}");
        } catch (System.Exception exception) {
            throw new System.Exception($"{Tag} : {exception}");
        }
    }

    /**
     * Permite llamar una funcion o procedimiento para crear o actualizar
     * Ademas regresa un valor primitivo a traves de NpgsqlDbType
     * <param name="query"></param>
     * <param name="outputName"></param>
     * <param name="outputType"></param>
     */
    protected T? ApplyCreateOrUpdate<T>(string query, string outputName, NpgsqlDbType outputType) {
        try {
            var command = ApplyParameters(true, query, GetParameters());
        
            command.Parameters.Add(new NpgsqlParameter(outputName, outputType) {
                Direction = ParameterDirection.Output
            });

            command.ExecuteNonQuery();

            var value = command.Parameters[outputName].Value;

            if (value != null && value != DBNull.Value) {
                return (T) Convert.ChangeType(value, typeof(T));
            }

            return default(T?);
        } catch (NpgsqlException exception) {
            throw new System.Exception($"{exception.Message}"); 
        } catch (System.Exception exception) {
            throw new System.Exception($"{Tag} : {exception}");
        }
    }

}