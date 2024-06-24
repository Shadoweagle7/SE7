using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Threading.Tasks
{
    public static class TaskEx
    {
        public static Task WhenSingle(params Task[] tasks) => WhenSingle(tasks.AsEnumerable());

        public static Task WhenSingle(IEnumerable<Task> tasks)
        {
            Task.WhenAll
        }
    }
}
