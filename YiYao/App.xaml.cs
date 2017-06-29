using LogService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WebService;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(Application));
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Error("CurrentDomain_UnhandledException"　+ e.ExceptionObject.ToString());
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            logger.Error("Current_DispatcherUnhandledException", e.Exception);
        }
    }
}
