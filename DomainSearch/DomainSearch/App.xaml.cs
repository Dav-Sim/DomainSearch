using DomainSearch.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DomainSearch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ////culture
            //var ownCulture = new CultureInfo("en-US");
            //ownCulture.DateTimeFormat = CultureInfo.GetCultureInfo("cs-CZ").DateTimeFormat;
            //System.Threading.Thread.CurrentThread.CurrentCulture = ownCulture;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = ownCulture;

            //show window
            MainWindow mainWin = new MainWindow();
            this.MainWindow = mainWin;
            mainWin.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(MainWindow, "An unhandled exception just occurred: " + e.Exception.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }
    }
}
