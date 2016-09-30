// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;
using System.Windows;

using Lyudmila.Client.Windows;

namespace Lyudmila.Client
{
    /// <summary>
    ///   Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void StartApp(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += delegate (object sender2, UnhandledExceptionEventArgs eargs)
            {
                var exception = (Exception)eargs.ExceptionObject;

                if (MessageBox.Show($"Unhandled exception:{Environment.NewLine}{exception.GetType()}: {exception.Message}", "Lyudmila", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Environment.Exit(1);
                }
            };

            if (Directory.Exists("Resources"))
            {
                if(!File.Exists("bass.dll"))
                    File.Move("Resources\\dep\\bass.dll", Path.Combine(Environment.CurrentDirectory, "bass.dll"));
                if (!File.Exists("bass_aac.dll"))
                    File.Move("Resources\\dep\\bass_aac.dll", Path.Combine(Environment.CurrentDirectory, "bass_aac.dll"));
                if (!File.Exists("Bass.Net.dll"))
                    File.Move("Resources\\dep\\Bass.Net.dll", Path.Combine(Environment.CurrentDirectory, "Bass.Net.dll"));
                Directory.Delete("Resources", true);
            }

            new MainWindow().Show();
        }
    }
}