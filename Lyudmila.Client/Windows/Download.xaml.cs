// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Lyudmila.Client.Properties;

using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Readers;

namespace Lyudmila.Client.Windows
{
    /// <summary>
    ///   Interaction logic for Download.xaml
    /// </summary>
    public partial class Download : INotifyPropertyChanged
    {
        private string _headerText;

        private readonly string _gameName;

        public Download(string gameName, string url)
        {
            InitializeComponent();
            HeaderText = $"Téléchargement: {gameName}";
            _gameName = gameName;

            if(File.Exists(Path.Combine(Environment.CurrentDirectory, "DL", url.Split('/').Last())))
            {
                new Thread(Extract).Start();
            }
            else
            {
                startDownload(url);
            }
        }

        private void startDownload(string url)
        {
            Task.Delay(1000);
            if(!Directory.Exists("DL"))
            {
                Directory.CreateDirectory("DL");
            }

            var thread = new Thread(() => {
                var client = new WebClient();
                client.DownloadProgressChanged += client_DownloadProgressChanged;
                client.DownloadFileCompleted += client_DownloadFileCompleted;
                client.DownloadFileAsync(new Uri(url), Path.Combine(Environment.CurrentDirectory, "DL", url.Split('/').Last()));
            });
            progressBar.IsIndeterminate = false;
            thread.Start();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Dispatcher.Invoke((MethodInvoker)delegate {
                var bytesIn = double.Parse(e.BytesReceived.ToString());
                var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                var percentage = bytesIn / totalBytes * 100;
                
                progressBar.Value = int.Parse(Math.Truncate(percentage).ToString(CultureInfo.InvariantCulture));
            });
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            new Thread(Extract).Start();
        }

        private void Extract()
        {
            Dispatcher.Invoke(() =>
            {
                progressBar.IsIndeterminate = true;
            });

            if (!Directory.Exists("Jeux"))
            {
                Directory.CreateDirectory("Jeux");
            }

            switch (_gameName) // TODO
            {
                case "AoE2HD":
                    ExtractCmd(Path.Combine(Environment.CurrentDirectory, "DL", "AoE2HD.zip"), Path.Combine(Environment.CurrentDirectory, "Jeux", "AoE2HD"));

                    Settings.Default.AoE2HD_Installed = true;
                    Settings.Default.AoE2HD_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", "AoE2HD");
                    break;
            }
            Dispatcher.Invoke(Close);
        }

        private static void ExtractCmd(string zipPath, string extractPath)
        {
            using (var archive = ZipArchive.Open(zipPath))
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(extractPath, new ExtractionOptions { ExtractFullPath = true, Overwrite = true });
                }
            }
        }

        public string HeaderText
        {
            get { return _headerText; }
            set
            {
                _headerText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HeaderText"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}