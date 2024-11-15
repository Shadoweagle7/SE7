using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.WinForms.DependencyInjection
{
    public interface IDependencyInjectibleForm
    {
        public IHost? Host { get; set; }
    }
}
