// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;
using System.Linq;

using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Readers;

namespace TestApp
{
    internal class ExampleCode
    {
        private void RarExtract()
        {
            var zipPath = @"D:\Downloads\Call_of_Duty_2_V5.1.rar";
            var extractPath = Path.Combine(Environment.CurrentDirectory, "CoD2");

            using(var archive = RarArchive.Open(zipPath))
            {
                foreach(var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(extractPath, new ExtractionOptions {ExtractFullPath = true, Overwrite = true});
                }
            }
        }
    }
}