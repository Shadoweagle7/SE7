namespace SE7.Utility.Try
{
    public delegate Task TryFuncAsync();
    public delegate Task TryFuncWithArgsAsync(TryArgs tryArgs);
    public delegate Task<T> TryFuncAsync<T>();
    public delegate Task<T> TryFuncWithArgsAsync<T>(TryArgs tryArgs);
}
