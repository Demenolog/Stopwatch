using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stopwatch.Services;
using Stopwatch.ViewModels;
using System;
using System.Windows;
using Stopwatch.ViewModels.Auxiliaries;

namespace Stopwatch
{
    public partial class App
    {
        private static IHost s_host;

        public static IServiceProvider Services => Host.Services;

        public static IHost Host => s_host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddViewModel();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;

            base.OnStartup(e);

            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            using (Host)
            {
                await Host.StopAsync();
            }
        }
    }
}