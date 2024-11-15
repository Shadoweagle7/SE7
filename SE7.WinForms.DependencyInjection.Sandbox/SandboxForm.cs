using Microsoft.Extensions.Hosting;

namespace SE7.WinForms.DependencyInjection.Sandbox
{
    public partial class SandboxForm : Form, IDependencyInjectibleForm
    {
        public IHost? Host { get; set; }

        public SandboxForm()
        {
            InitializeComponent();
        }
    }
}
