using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Utility.Try
{
    public static class TryExtensions
    {
        public static TryResult Try(this TryAction tryAction)
        {
			try
			{
				tryAction();

				return TryResult.Success;
			}
			catch (Exception e)
			{
                return e;
			}
        }

        public static TryResult Try(this TryActionWithArgs tryActionWithArgs, TryArgs tryArgs)
        {
            try
            {
                tryActionWithArgs(tryArgs);

                return TryResult.Success;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static TryResult<T> Try<T>(this TryFunc<T> tryFunc)
		{
            try
            {
                return tryFunc();
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static TryResult<T> Try<T>(this TryFuncWithArgs<T> tryFuncWithArgs, TryArgs tryArgs)
        {
            try
            {
                return tryFuncWithArgs(tryArgs);
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static async Task<TryResult> TryAsync(this TryFuncAsync tryFuncAsync)
        {
            try
            {
                await tryFuncAsync();

                return TryResult.Success;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static async Task<TryResult> TryAsync(this TryFuncWithArgsAsync tryFuncWithArgsAsync, TryArgs tryArgs)
        {
            try
            {
                await tryFuncWithArgsAsync(tryArgs);

                return TryResult.Success;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static async Task<TryResult<T>> TryAsync<T>(this TryFuncAsync<T> tryFuncAsync)
        {
            try
            {
                return await tryFuncAsync();
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static async Task<TryResult<T>> TryAsync<T>(this TryFuncWithArgsAsync<T> tryFuncWithArgsAsync, TryArgs tryArgs)
        {
            try
            {
                return await tryFuncWithArgsAsync(tryArgs);
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static async ValueTask<TryResult> TryAsync(this TryFuncValueAsync tryFuncValueAsync)
        {
            try
            {
                await tryFuncValueAsync();

                return TryResult.Success;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static async ValueTask<TryResult> TryAsync(this TryFuncWithArgsValueAsync tryFuncWithArgsValueAsync, TryArgs tryArgs)
        {
            try
            {
                await tryFuncWithArgsValueAsync(tryArgs);

                return TryResult.Success;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static async ValueTask<TryResult<T>> TryAsync<T>(this TryFuncValueAsync<T> tryFuncValueAsync)
        {
            try
            {
                return await tryFuncValueAsync();
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static async ValueTask<TryResult<T>> TryAsync<T>(this TryFuncWithArgsValueAsync<T> tryFuncWithArgsValueAsync, TryArgs tryArgs)
        {
            try
            {
                return await tryFuncWithArgsValueAsync(tryArgs);
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}
