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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            _instace = this;
            HealthDataCenter = new HealthDataCenter();
        }

        public HealthDataCenter HealthDataCenter { get; set; }

        public static App Instance
        {
            get
            {
                return _instace;
            }
        }

        private static App _instace;
    }
}
