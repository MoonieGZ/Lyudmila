// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.Text.RegularExpressions;
using System.Windows;

using Lyudmila.Properties;
using Lyudmila.Windows;

namespace Lyudmila.Flyouts
{
    /// <summary>
    ///   Interaction logic for NickChanger.xaml
    /// </summary>
    public partial class NickChanger
    {
        public NickChanger()
        {
            InitializeComponent();
        }

        private void ChangeNick_Click(object sender, RoutedEventArgs e)
        {
            if(VerifyName(NickBox.Text))
            {
                Settings.Default.Username = NickBox.Text;
                ((MainWindow) Application.Current.MainWindow).NicknameFlyout.IsOpen = false;
            }
        }

        private static bool VerifyName(string username)
            =>
                username.Length <= 16 && username.Length >= 4 && Regex.IsMatch(username, @"^[a-zA-Z0-9_\-\[\]\s]+$") && !Regex.IsMatch(username, @"^[\s]+$")
                && !username.ToLower().Contains("Admin") && !username.ToLower().Equals("Nickname");
    }
}