using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


using Hardcodet.Wpf.TaskbarNotification;

namespace app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private TaskbarIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");

            var cfg = AppConfiguration.GetCustomSection();
            var worker = Worker.Instance;
            worker.PortName = cfg.Port;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose();
            base.OnExit(e);
        }


    }
}
