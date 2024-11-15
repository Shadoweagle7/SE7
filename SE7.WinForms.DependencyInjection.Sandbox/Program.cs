using Microsoft.Extensions.Hosting;
using System.Windows.Forms;

namespace SE7.WinForms.DependencyInjection.Sandbox
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var sandboxForm = new SandboxForm()
            {
                Host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {

                })
                .Build()
            };

            Application.Run(sandboxForm);
        }
    }
}