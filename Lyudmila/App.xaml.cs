// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Windows;

using Lyudmila.Windows;

namespace Lyudmila
{
    /// <summary>
    ///   Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += delegate (object sender2, UnhandledExceptionEventArgs eargs)
            {
                var exception = (Exception)eargs.ExceptionObject;

                if (MessageBox.Show($"Unhandled exception: {exception}", "Lyudmila", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Environment.Exit(1);
                }
            };

            new MainWindow().Show();
        }
    }
}