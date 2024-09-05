using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SE7.Sql.Services.Implementations
{
    internal class MicrosoftSqlServerService
    {
        private readonly string ConnectionString;

        public MicrosoftSqlServerService(IConfiguration configuration)
        {

        }

        public StoredProcedureBuilderWithCommand Get<TDataTransferObject>()
        {
            var dataTransferObjectProperties = typeof(TDataTransferObject)
                .GetProperties()
                .Where(p => p.GetCustomAttribute<SqlServerIgnorePropertyAttribute>() == null)
            ;

            foreach (var dataTransferObjectProperty in dataTransferObjectProperties)
            {
                if (
                    dataTransferObjectProperty.GetMethod == null ||
                    dataTransferObjectProperty.SetMethod == null
                )
                {
                    throw new InvalidOperationException(
                        $"Property {dataTransferObjectProperty.Name} of type {typeof(TDataTransferObject).FullName ?? typeof(TDataTransferObject).Name} requires both a get and set method."
                    );
                }
            }

            Get<int>().By<SE7.Sql.Selectors.Int32>(27).ExecuteAsync();
            Get<int>().By<SE7.Sql.Selectors.Name>("").And<Selectors.Guid>(Guid.Empty).ExecuteAsync();

            return new StoredProcedureBuilderWithCommand(
                ConnectionString,
                typeof(TDataTransferObject)
            );
        }
    }
}
