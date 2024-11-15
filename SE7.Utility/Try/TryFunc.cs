namespace SE7.Utility.Try
{
    public delegate T TryFunc<T>();
    public delegate T TryFuncWithArgs<T>(TryArgs tryArgs);
}
