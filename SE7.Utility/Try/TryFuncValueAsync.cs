namespace SE7.Utility.Try
{
    public delegate ValueTask TryFuncValueAsync();
    public delegate ValueTask TryFuncWithArgsValueAsync(TryArgs tryArgs);
    public delegate ValueTask<T> TryFuncValueAsync<T>();
    public delegate ValueTask<T> TryFuncWithArgsValueAsync<T>(TryArgs tryArgs);
}
