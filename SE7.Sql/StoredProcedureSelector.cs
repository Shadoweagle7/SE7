namespace SE7.Sql
{
    public abstract record StoredProcedureSelector
    {
        public abstract object GetTypeErasedValue();
        public virtual string GetParameterName() => GetType().Name;
    }
}
