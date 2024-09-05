using SE7.Utility.Try;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Utility.Extensions.Threading
{
    public readonly struct EmptyOptionTask
    {
        private static readonly Task CompletedTask = Task.CompletedTask;

        public readonly TaskAwaiter GetAwaiter() => CompletedTask.GetAwaiter();
    }

    public readonly struct OptionTask
    {
        public static EmptyOptionTask None { get; }
    }

    public static class OptionTaskExtensions
    {
        public static async Task<Option<TResult>> SelectAsync<T, TResult>(this Option<T> option, Func<T, Task<TResult>> selector)
        {
            if (option.HasValue)
            {
                return new Option<TResult>(await selector(option.Value));
            }

            return Option.None;
        }

        public static async Task<Option<TResult>> SelectAsync<T, TResult>(
            this Task<Option<T>> taskOfOptionOfT,
            Func<T, Task<TResult>> selector
        )
        {
            var optionOfT = await taskOfOptionOfT;

            if (optionOfT.HasValue)
            {
                return await selector(optionOfT.Value);
            }

            return Option.None;
        }

        public static async Task<Option<TResult>> MatchAsync<T, TResult>(
            this Option<T> option,
            Func<T, Task<TResult>> onValuePresentAsync,
            Func<Task<TResult>> onEmptyAsync
        )
        {
            if (option.HasValue)
            {
                return new Option<TResult>(await onValuePresentAsync(option.Value));
            }

            return new Option<TResult>(await onEmptyAsync());
        }

        public static async Task<Option<TResult>> MatchAsync<T, TResult>(
            this Option<T> option,
            Func<T, EmptyOptionTask> onValuePresentAsync,
            Func<Task<TResult>> onEmptyAsync
        )
        {
            if (option.HasValue)
            {
                await onValuePresentAsync(option.Value);

                return new Option<TResult>();
            }

            return new Option<TResult>(await onEmptyAsync());
        }

        public static async Task<Option<TResult>> MatchAsync<T, TResult>(
            this Option<T> option,
            Func<T, Task<TResult>> onValuePresentAsync,
            Func<EmptyOptionTask> onEmptyAsync
        )
        {
            if (option.HasValue)
            {
                return new Option<TResult>(await onValuePresentAsync(option.Value));
            }

            await onEmptyAsync();

            return new Option<TResult>();
        }

        //

        public static async Task<Option<TResult>> MatchAsync<T, TResult>(
            this Task<Option<T>> taskOfOptionOfT,
            Func<T, Task<TResult>> onValuePresentAsync,
            Func<Task<TResult>> onEmptyAsync
        )
        {
            var optionOfT = await taskOfOptionOfT;

            if (optionOfT.HasValue)
            {
                return new Option<TResult>(await onValuePresentAsync(optionOfT.Value));
            }

            return new Option<TResult>(await onEmptyAsync());
        }

        public static async Task<Option<TResult>> MatchAsync<T, TResult>(
            this Task<Option<T>> taskOfOptionOfT,
            Func<T, EmptyOptionTask> onValuePresentAsync,
            Func<Task<TResult>> onEmptyAsync
        )
        {
            var optionOfT = await taskOfOptionOfT;

            if (optionOfT.HasValue)
            {
                await onValuePresentAsync(optionOfT.Value);

                return new Option<TResult>();
            }

            return new Option<TResult>(await onEmptyAsync());
        }

        public static async Task<Option<TResult>> MatchAsync<T, TResult>(
            this Task<Option<T>> taskOfOptionOfT,
            Func<T, Task<TResult>> onValuePresentAsync,
            Func<EmptyOptionTask> onEmptyAsync
        )
        {
            var optionOfT = await taskOfOptionOfT;

            if (optionOfT.HasValue)
            {
                return new Option<TResult>(await onValuePresentAsync(optionOfT.Value));
            }

            await onEmptyAsync();

            return new Option<TResult>();
        }
    }
}
