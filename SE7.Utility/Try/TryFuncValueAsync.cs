using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Utility.Try
{
    public delegate ValueTask TryFuncValueAsync();
    public delegate ValueTask TryFuncWithArgsValueAsync(TryArgs tryArgs);
    public delegate ValueTask<T> TryFuncValueAsync<T>();
    public delegate ValueTask<T> TryFuncWithArgsValueAsync<T>(TryArgs tryArgs);
}
