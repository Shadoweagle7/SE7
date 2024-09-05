namespace SE7.Sql.Selectors
{
    public record Id<TValue>(TValue Value) : StoredProcedureSelector
    {
        public override object GetTypeErasedValue()
        {
            if (Value == null)
            {
                return DBNull.Value;
            }

            return Value;
        }
    }

    public record Int32(int Value) : Id<int>(Value)
    {
        public static implicit operator Int32(int value) => new(value);
        public static implicit operator int(Int32 id) => id.Value;
    }

    public record Guid(System.Guid Value) : Id<System.Guid>(Value)
    {
        public static implicit operator Guid(System.Guid value) => new(value);
        public static implicit operator System.Guid(Guid guid) => guid.Value;
    }
}
