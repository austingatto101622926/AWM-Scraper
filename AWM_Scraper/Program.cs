using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace AWM_Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "https://www.awm.gov.au/";
            int page = 162;
            int ppp = 5000;

            int count = 0;

            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\STUDENT\list3.txt");
            //file.AutoFlush = true;

            while (page < 163)
            {
                List<string> entries = new List<string>();
                HtmlAgilityPack.HtmlDocument doc = web.Load($"{baseAddress}advanced-search?query=&people=true&facet_related_conflict_sort=8%3AFirst+World+War%2C+1914-1918&page={page}&ppp={ppp}");

                var listings = doc.DocumentNode.SelectNodes("//h3[@class='listing__title']/a");
                if (listings != null)
                {
                    for (int i = 0; i < listings.Count; i++)
                    {
                        string name = listings[i].InnerHtml.Trim();
                        name = name.Replace("&#039;", "'");
                        string link = listings[i].Attributes.First().Value;
                        string entry = $"{name}\t{link}";
                        entries.Add(entry);
                        count++;
                    }

                    for (int i = 0; i < entries.Count; i++)
                    {
                        Console.WriteLine(entries[i]);
                        file.WriteLine(entries[i]);
                    }
                    page++;
                }
                Console.WriteLine(count);
            }
            Console.ReadLine();
        }
    }

    public struct Veteran
    {
        public string Name;
        public string Link;

        public Veteran(string name, string link)
        {
            Name = name;
            Link = link;
        }
    }
}
