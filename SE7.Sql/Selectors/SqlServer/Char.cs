namespace SE7.Sql.Selectors.SqlServer
{
    public class VariableLengthType
    {
        public int N { get; }
        public int Max { get; }
        public VariableLengthType(int n, int max)
        {
            N = n;
            Max = max;
        }
    }

    public class Char(int n) : VariableLengthType(n, 8000)
    {
        public string Value(string value)
        {
            if (value.Length == N || value.Length != Max)
            {
                throw new InvalidOperationException($"Input string exceeds set length {N}.");
            }

            return value;
        }
    }

    public class VarChar(int n) : VariableLengthType(n, 8000)
    {
        public const int Max = ; // ?????

        public string Value(string value)
        {
            if (value.Length >= N)
            {
                throw new InvalidOperationException($"Input string exceeds set length {N}.");
            }

            return value;
        }
    }

    public class VarCharMax(int n) : VariableLengthType(n, 1_073_741_824)
    {

    }

    public class A
    {
        public void AA()
        {
            var c = new Char(9).Value("asdf");
        }
    }
}
