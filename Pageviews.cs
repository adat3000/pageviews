using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;

namespace pageviews
{
    class Pageviews : IPageviews
    {
        List<AllHours> table = new List<AllHours>();

        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public List<AllHours> ConvertToList(string filePath)
        {
            string line;
            StreamReader file = new StreamReader(filePath);
            List<AllHours> tbl = new List<AllHours>();

            try
            {
                while ((line = file.ReadLine()) != null)
                {
                    var cols = line.Split(' ');

                    AllHours dr = new AllHours
                    {
                        Domain_code = cols[0],
                        Page_title = cols[1],
                        Count_views = Convert.ToInt32(cols[2])
                    };

                    tbl.Add(dr);
                }
                file.Close();
            }
            catch (Exception ex)
            {
                file.Close();
                Console.WriteLine("Can not read the record ! " + ex.Message);
            }

            return tbl;
        }

        public void DownloadFiles()
        {
            try
            {
                WebClient file = new WebClient();
                string urlFileName = "", fileName = "";
                string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                for (int i = 0; i < 5; i++)
                {
                    urlFileName = "https://dumps.wikimedia.org/other/pageviews/" + Year + "/" + Year + "-" + Month + "/";
                    fileName = "pageviews-" + Year + Month + Day + "-" + (19 + i).ToString() + "0000.gz";
                    if (!File.Exists(directoryPath + "\\" + fileName))
                    {
                        file.DownloadFile(urlFileName + fileName, fileName);
                        Console.WriteLine("  Downloaded: {0}", fileName);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DecompressFiles()
        {
            string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);

            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
            {
                if (!File.Exists(directoryPath + "\\" + fileToDecompress.Name.Substring(0, 25)))
                {
                    using FileStream originalFileStream = fileToDecompress.OpenRead();
                    string currentFileName = fileToDecompress.FullName;
                    string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                    using FileStream decompressedFileStream = File.Create(newFileName);
                    using GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress);
                    decompressionStream.CopyTo(decompressedFileStream);
                    Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                }
            }
        }

        public void MergeFiles()
        {
            WebClient file = new WebClient();
            string fileName = "";
            string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            List<AllHours> pageviews = new List<AllHours>();
            table = new List<AllHours>();

            for (int i = 0; i < 5; i++)
            {
                fileName = "pageviews-" + Year + Month + Day + "-" + (19 + i).ToString() + "0000";
                pageviews = ConvertToList(directoryPath + "\\" + fileName);
                pageviews = pageviews.GroupBy(x => new AllHours { Domain_code = x.Domain_code, Page_title = x.Page_title })
                                .Select(g => new AllHours { Domain_code = g.Key.Domain_code, Page_title = g.Key.Page_title, CNT = g.Sum(x => x.Count_views) }).ToList();
                table.AddRange(pageviews);
                Console.WriteLine("      Merged: {0}", fileName);
            }
            Console.WriteLine("Count = " + table.Count);
        }

        public void ProcessQuery()
        {
            table = table.Select(g => new AllHours { Domain_code = g.Domain_code, Page_title = g.Page_title, CNT = g.CNT })
                            .OrderByDescending(y => y.CNT).Take(100).ToList();
            for (int i = 0; i < table.Count; i++)
            {
                Console.WriteLine((i + 1) + " " + table[i].Domain_code + " | " + table[i].Page_title + " | " + table[i].CNT);
            }
            Console.WriteLine("Count = " + table.Count);
        }
    }
}
