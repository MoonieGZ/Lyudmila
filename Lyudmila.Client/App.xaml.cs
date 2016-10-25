// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;

using Lyudmila.Client.Properties;
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
            AppDomain.CurrentDomain.UnhandledException += delegate(object sender2, UnhandledExceptionEventArgs eargs)
            {
                var exception = (Exception) eargs.ExceptionObject;

                if(MessageBox.Show($"Unhandled exception:{Environment.NewLine}{exception.GetType()}: {exception.Message}", "Lyudmila", MessageBoxButton.OK,
                       MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Environment.Exit(1);
                }
            };

            if(e.Args.Length == 0)
            {
#if !DEBUG
                try
                {
                    var newHash = new WebClient().DownloadString(new Uri($"http://{Settings.Default.ServerIP}/logiciels/launcher/hash.md5")).ToLower();
                    var currentHash = GetHash(Assembly.GetEntryAssembly().Location).ToLower();
                    
                    if(newHash.Equals(currentHash))
                    {
                        new MainWindow().Show();
                    }
                    else
                    {
                        var updating = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Lyudmila\\updating.vbs";
                        using (
                            var resource =
                                Assembly.GetExecutingAssembly()
                                    .GetManifestResourceStream("Lyudmila.Client.Resources.scripts.updating.vbs"))
                        {
                            using (var updater = new FileStream(updating, FileMode.Create, FileAccess.Write))
                            {
                                resource?.CopyTo(updater);
                            }
                        }
                        Process.Start(updating);

                        var me = Assembly.GetExecutingAssembly().Location;
                        File.Copy(me, "Updating_Lyudmila.exe");
                        Process.Start("Updating_Lyudmila.exe", "-update");
                        Environment.Exit(0);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"{ex.GetType()}: {ex.Message}");
                    Environment.Exit(1);
                }
#else
                new MainWindow().Show();
#endif
            }
            else
            {
                if (e.Args.First() == "-update")
                {
                    Thread.Sleep(2000);
                    File.Delete("Lyudmila.Client.exe");
                    new WebClient().DownloadFile(new Uri($"http://{Settings.Default.ServerIP}/logiciels/launcher/Lyudmila.Client.exe"),
                        @"Lyudmila.Client.exe");
                    Process.Start("Lyudmila.Client.exe", "-run");
                    Environment.Exit(0);
                }
                if (e.Args.First() == "-run")
                {
                    Thread.Sleep(2000);
                    File.Delete("Updating_Lyudmila.exe");
                    new MainWindow().Show();
                }
            }
        }

        private static string GetHash(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }
    }
}