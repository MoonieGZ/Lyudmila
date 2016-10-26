// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;
using System.IO.Compression;

namespace TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var zipPath = @"CoD4.zip";
            var extractPath = Path.Combine(Environment.CurrentDirectory, "Jeux");
            var dirName = Path.Combine(extractPath, Path.GetFileNameWithoutExtension(zipPath));

            using(var archive = ZipFile.OpenRead(zipPath))
            {
                foreach(var entry in archive.Entries)
                {
                    if(entry.FullName.EndsWith("/"))
                    {
                        ZipFile.ExtractToDirectory(zipPath, extractPath);
                        break;
                    }
                    if(!Directory.Exists(dirName))
                    {
                        Directory.CreateDirectory(dirName);
                        ZipFile.ExtractToDirectory(zipPath, dirName);
                        break;
                    }
                }
            }
            Console.ReadKey();
        }
    }
}