// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

namespace Lyudmila.Flyouts
{
    /// <summary>
    ///   Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification
    {
        public Notification()
        {
            InitializeComponent();
        }

        public void Display(string message)
        {
            Dispatcher.Invoke(() => { Notifier.Text = message; });
        }
    }
}