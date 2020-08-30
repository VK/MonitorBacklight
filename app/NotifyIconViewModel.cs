
using System.Windows;
using System.Windows.Input;
using System;



namespace app
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel
    {
        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
            }
        }


        private String _port  = null;

        public String Port {

         get {
             if(this._port == null) {
                 var cfg =  AppConfiguration.GetCustomSection();
                 this._port = cfg.Port;
             }
             return this._port;
         }
         set {
             this._port = value;
         }

        }






    }
}