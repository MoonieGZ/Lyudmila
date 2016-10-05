// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;

using Lyudmila.Client.Windows;

using MaterialDesignThemes.Wpf;

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

            if (File.Exists("easteregg.ini"))
            {
                var color = string.Empty;
                var accent = string.Empty;
                var file = File.ReadAllLines("easteregg.ini");
                if (file.Length.Equals(2)) // Color, Accent
                {
                    color = file[0];
                    accent = file[1];
                }
                if (file.Length.Equals(1)) // Color
                {
                    color = file[0];
                }
                try
                {
                    var accentList = new List<string>();
                    var colorList = new List<string>();

                    var assembly = Assembly.GetExecutingAssembly();
                    using (var stream = assembly.GetManifestResourceStream("Lyudmila.Client.Resources.Colors.txt"))
                    {
                        if (stream != null)
                            using (var reader = new StreamReader(stream))
                            {
                                while (reader.Peek() >= 0)
                                {
                                    colorList.Add(reader.ReadLine());
                                }
                            }
                    }
                    using (var stream = assembly.GetManifestResourceStream("Lyudmila.Client.Resources.Accents.txt"))
                    {
                        if (stream != null)
                            using (var reader = new StreamReader(stream))
                            {
                                while (reader.Peek() >= 0)
                                {
                                    accentList.Add(reader.ReadLine());
                                }
                            }
                    }

                    if (!string.IsNullOrEmpty(color) && colorList.Contains(color))
                    {
                        new PaletteHelper().ReplacePrimaryColor(color);
                    }

                    if (!string.IsNullOrEmpty(accent) && accentList.Contains(accent))
                    {
                        new PaletteHelper().ReplaceAccentColor(accent);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Easter bunny doesn't like you for some reason...");
                    throw;
                }
            }

            new MainWindow().Show();
        }
    }
}