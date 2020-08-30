using System.Windows.Controls;
using System.Linq;
using System.Windows;


using System.Diagnostics;

namespace app
{
    public class PortRadioItem : MenuItem
    {



        protected override void OnVisualParentChanged(DependencyObject parent)
        {
            var ic = Parent as MenuItem;
            if (ic != null && ic.Header != null)
                IsChecked = ic.Header.Equals(this.Header);
        }

        protected override void OnClick()
        {
            var ic = Parent as MenuItem;

            if (null != ic)
            {
                var rmi = ic.Items.OfType<PortRadioItem>().FirstOrDefault(i => i.IsChecked);
                if (null != rmi) rmi.IsChecked = false;

                IsChecked = true;
                ic.Header = this.Header;

                var cfg = AppConfiguration.GetCustomSection();
                cfg.Port = (string)this.Header;
                AppConfiguration.SaveConfig(cfg);


                var worker = Worker.Instance;
                worker.PortName = cfg.Port;

            }
            base.OnClick();
        }


    }
}