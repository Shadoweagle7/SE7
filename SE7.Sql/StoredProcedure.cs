using Microsoft.Data.SqlClient;
using System.Collections.Immutable;

namespace SE7.Sql
{
    public class StoredProcedure(string connectionString, Type dataTransferObjectType)
    {
        protected Type DataTransferObjectType { get; } = dataTransferObjectType;
        public string ConnectionString { get; } = connectionString;

        protected virtual IList<StoredProcedureSelector> GetStoredProcedureSelectors() => [];

        public async Task ExecuteAsync()
        {
            await using var sqlConnection = new SqlConnection(ConnectionString);

            await sqlConnection.OpenAsync();

            var command = sqlConnection.CreateCommand();

            command.CommandType = System.Data.CommandType.StoredProcedure;

            var parameters = GetStoredProcedureSelectors()
                .Select(s => new SqlParameter(s.GetParameterName(), s.GetTypeErasedValue()))
            ;

            command.Parameters.AddRange(parameters.ToArray());

            await using var reader = await command.ExecuteReaderAsync();
            var properties = DataTransferObjectType
                .GetProperties()
                .ToImmutableDictionary(p => p.Name, p => p);
            ;

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (properties.TryGetValue(reader.GetName(i), out var property))
                {

                }
            }
        }
    }
}
