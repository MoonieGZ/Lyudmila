// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Timers;
using System.Windows;

using Lyudmila.Client.Windows;

namespace Lyudmila.Client.Helpers
{
    internal class ClockUpdater
    {
        private static Timer t;

        public static void Start()
        {
            t = new Timer {AutoReset = false};
            t.Elapsed += t_Elapsed;
            t.Interval = GetInterval();
            t.Start();
        }

        private static double GetInterval()
        {
            var now = DateTime.Now;
            return (60 - now.Second) * 1000 - now.Millisecond;
        }

        private static void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => { ((MainWindow) Application.Current.MainWindow).StatusBarClock = DateTime.Now.ToShortTimeString(); });

            t.Interval = GetInterval();
            t.Start();
        }
    }
}