namespace SE7.Utility
{
    public interface ITypeTestable
    {
        Option<TResult> As<TResult>();
        bool Is<TOther>();
        bool Is<TOther>(out Option<TOther> other);
    }
}