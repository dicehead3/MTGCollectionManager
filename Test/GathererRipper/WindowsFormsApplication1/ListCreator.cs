using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class ListCreator
    {
        public static DateTime startTime;
        public static DateTime endTime;

        private static List<CardData> cards = new List<CardData>();
        public static List<CardData> CreateCardList()
        {
            int cardCount = 1636;
            Task[] tasks = new Task[cardCount];
            int counter = 0;
            startTime = DateTime.Now;
            for (; counter < cardCount; counter++)
            {
                int c = counter;
                var task = Task.Factory.StartNew(() => GetTitle(c), TaskCreationOptions.LongRunning);
                tasks[c] = task;
            }

            Task.WaitAll(tasks);
            endTime = DateTime.Now;
            return cards;
        }


        private static void GetTitle(int id)
        {
            var req = (HttpWebRequest) WebRequest
                .Create(string.Format("http://gatherer.wizards.com/Pages/Card/Details.aspx?multiverseid={0}", id));
            var resp = (HttpWebResponse) req.GetResponse();
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                var stream = resp.GetResponseStream();
                var res = resp.CharacterSet == null
                    ? new StreamReader(stream).ReadToEnd()
                    : new StreamReader(stream, Encoding.GetEncoding(resp.CharacterSet)).ReadToEnd();
                resp.Close();
                stream.Close();

                var match = Regex.Match(res, @"<title>\s*(.+?)\s*</title>");
                if (match.Success)
                {
                    var title = match.Groups[1].Value;
                    title = title.Remove(title.IndexOf("Gatherer"));
                    if (!string.IsNullOrEmpty(title))
                    {
                        var cd = new CardData
                        {
                            Id = id,
                            Title = title.Remove(title.Length - 3)
                        };
                        cards.Add(cd);
                    }
                }
            }
        }
    }
}
