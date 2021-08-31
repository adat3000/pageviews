using System;
using System.Collections.Generic;
using System.Text;

namespace pageviews
{
    interface IPageviews
    {
        List<AllHours> ConvertToList(string filePath);
        void DownloadFiles();
        void DecompressFiles();
        void MergeFiles();
        void ProcessQuery();
    }
}
