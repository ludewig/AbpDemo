using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Abp;

namespace AbpDemo.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly AbpBootstrapper _bootstrapper;
        private MainWindow _window;
        public App()
        {
            _bootstrapper = AbpBootstrapper.Create<AbpDemoClientModule>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _bootstrapper.Initialize();
            _window = _bootstrapper.IocManager.Resolve<MainWindow>();
            _window.Show();
            //base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _bootstrapper.IocManager.Release(_window);
            _bootstrapper.Dispose();
            //base.OnExit(e);
        }
    }
}
