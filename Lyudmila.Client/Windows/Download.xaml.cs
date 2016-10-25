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
        private readonly string _gameName;
        private string _headerText;

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

        private void startDownload(string url)
        {
            Task.Delay(1000);
            if(!Directory.Exists("DL"))
            {
                Directory.CreateDirectory("DL");
            }

            var thread = new Thread(() =>
            {
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
            Dispatcher.Invoke((MethodInvoker) delegate
            {
                var bytesIn = double.Parse(e.BytesReceived.ToString());
                var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                var percentage = bytesIn / totalBytes * 100;

                progressBar.Value = int.Parse(Math.Truncate(percentage).ToString(CultureInfo.InvariantCulture));
            });
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) => new Thread(Extract).Start();

        private void Extract()
        {
            Dispatcher.Invoke(() => { progressBar.IsIndeterminate = true; });

            if(!Directory.Exists("Jeux"))
            {
                Directory.CreateDirectory("Jeux");
            }

            ExtractCmd(Path.Combine(Environment.CurrentDirectory, "DL", $"{_gameName}.zip"), Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName));

            switch(_gameName)
            {
                case "AoE2HD":
                    Settings.Default.AoE2HD_Installed = true;
                    Settings.Default.AoE2HD_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "BF3":
                    Settings.Default.BF3_Installed = true;
                    Settings.Default.BF3_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "Blur":
                    Settings.Default.Blur_Installed = true;
                    Settings.Default.Blur_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "CoD2":
                    Settings.Default.CoD2_Installed = true;
                    Settings.Default.CoD2_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "CoD4":
                    Settings.Default.CoD4_Installed = true;
                    Settings.Default.CoD4_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "CoD5":
                    Settings.Default.CoD5_Installed = true;
                    Settings.Default.CoD5_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "CSGO":
                    Settings.Default.CSGO_Installed = true;
                    Settings.Default.CSGO_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "DoTA2":
                    Settings.Default.DoTA2_Installed = true;
                    Settings.Default.DoTA2_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "DoDS":
                    Settings.Default.DoDS_Installed = true;
                    Settings.Default.DoDS_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "F2":
                    Settings.Default.F2_Installed = true;
                    Settings.Default.F2_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "L4D2":
                    Settings.Default.L4D2_Installed = true;
                    Settings.Default.L4D2_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "SC2":
                    Settings.Default.SC2_Installed = true;
                    Settings.Default.SC2_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "SWJK2":
                    Settings.Default.SWJK2_Installed = true;
                    Settings.Default.SWJK2_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "TF2":
                    Settings.Default.TF2_Installed = true;
                    Settings.Default.TF2_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
                case "UT3":
                    Settings.Default.UT3_Installed = true;
                    Settings.Default.UT3_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", _gameName);
                    break;
            }

            Dispatcher.Invoke(Close);
        }

        private static void ExtractCmd(string zipPath, string extractPath)
        {
            using(var archive = ZipArchive.Open(zipPath))
            {
                foreach(var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(extractPath, new ExtractionOptions {ExtractFullPath = true, Overwrite = true});
                }
            }
        }
    }
}