using System;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using FriendOrganizer2.UI.Startup;

namespace FriendOrganizer2.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private void Application_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An army of monkies broke tha app, please do panik now!"
                            + Environment.NewLine + e.Exception.Message, "Unexpected panic..");
            e.Handled = true;
        }
    }
}
