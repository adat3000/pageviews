using System;
using System.Collections.Generic;
using System.Text;

namespace pageviews
{
    class Program
    {
        static void ShowUsage()
        {
            Console.WriteLine();
            Console.WriteLine("Incorrect arguments.");
            Console.WriteLine("Usage:");
            Console.WriteLine(" pageviews <Year> <Month> <Day>");
            Console.WriteLine("Example:");
            Console.WriteLine(" pageviews 2020 10 7");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            try
            {
                DateTime date;
                DateTime first = new DateTime(2015, 7, 1);
                Pageviews pageviews = new Pageviews();
                if (args.Length == 3)
                {
                    date = new DateTime(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
                    if (date >= first && date <= DateTime.Now)
                    {
                        pageviews.Year = args[0];

                        if (int.Parse(args[1]) < 10)
                        {
                            pageviews.Month = "0" + int.Parse(args[1]);
                        }
                        else
                        {
                            pageviews.Month = args[1];
                        }

                        if (int.Parse(args[2]) < 10)
                        {
                            pageviews.Day = "0" + int.Parse(args[2]);
                        }
                        else
                        {
                            pageviews.Day = args[2];
                        }

                        pageviews.DownloadFiles();
                        pageviews.DecompressFiles();
                        pageviews.MergeFiles();
                        pageviews.ProcessQuery();
                    }
                    else
                    {
                        Console.WriteLine("Min Date: 2015 7 1.");
                        ShowUsage();
                    }
                }
                else
                {
                    ShowUsage();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid arguments. " + e.Message);
                ShowUsage();
            }
        }
    }
}
