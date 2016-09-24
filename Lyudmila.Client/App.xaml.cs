// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

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
            new MainWindow().Show();
        }
    }
}