namespace SE7.Sql
{
    public class StoredProcedureBuilderWithCommand : StoredProcedure
    {
        public StoredProcedureBuilderWithCommand(
            string connectionString,
            Type dataTransferObjectType
        ) : base(connectionString, dataTransferObjectType) { }

        public StoredProcedureBuilderWithCommandAndOneOrMoreSelectors By<TStoredProcedureSelector>(
            TStoredProcedureSelector storedProcedureSelector
        )
            where TStoredProcedureSelector : StoredProcedureSelector
        {
            return new StoredProcedureBuilderWithCommandAndOneOrMoreSelectors(
                ConnectionString,
                DataTransferObjectType,
                storedProcedureSelector
            );
        }
    }
}
