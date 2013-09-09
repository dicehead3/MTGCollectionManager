using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //const string url = "http://gatherer.wizards.com/Pages/Card/Details.aspx?multiverseid={0}";
            //var cards = new List<CardData>();
            //var threads = new List<Thread>();
            //var startTime = DateTime.Now;
            //for (var i = 0; i < 5000; i += 6)
            //{
            //    var t1 = new Thread(() =>
            //    {
            //        var result = GetCardDetails(string.Format(url, i));
            //        if (!string.IsNullOrEmpty(result))
            //            cards.Add(new CardData { Id = i, Title = result });
            //        Thread.Sleep(50);
            //    });

            //    var t2 = new Thread(() =>
            //    {
            //        var result = GetCardDetails(string.Format(url, i + 1));
            //        if (!string.IsNullOrEmpty(result))
            //            cards.Add(new CardData { Id = i + 1, Title = result });
            //    });

            //    var t3 = new Thread(() =>
            //    {
            //        var result = GetCardDetails(string.Format(url, i + 2));
            //        if (!string.IsNullOrEmpty(result))
            //            cards.Add(new CardData { Id = i + 2, Title = result });
            //    });

            //    var t4 = new Thread(() =>
            //    {
            //        var result = GetCardDetails(string.Format(url, i + 3));
            //        if (!string.IsNullOrEmpty(result))
            //            cards.Add(new CardData { Id = i + 3, Title = result });
            //    });

            //    var t5 = new Thread(() =>
            //    {
            //        var result = GetCardDetails(string.Format(url, i + 4));
            //        if (!string.IsNullOrEmpty(result))
            //            cards.Add(new CardData { Id = i + 4, Title = result });
            //    });

            //    var t6 = new Thread(() =>
            //    {
            //        var result = GetCardDetails(string.Format(url, i + 5));
            //        if (!string.IsNullOrEmpty(result))
            //            cards.Add(new CardData { Id = i + 5, Title = result });
            //    });

            //    t1.Start();
            //    t2.Start();
            //    t3.Start();
            //    t4.Start();
            //    t5.Start();
            //    t6.Start();

            //    t1.Join();
            //    t2.Join();
            //    t3.Join();
            //    t4.Join();
            //    t5.Join();
            //    t6.Join();
            //}

            //foreach (var c in cards.OrderBy(x => x.Id).ToList())
            //{
            //    listBox1.Items.Add(string.Format("{0} -- {1}", c.Id, c.Title));
            //}

            //var stopTime = DateTime.Now;
            //var difference = stopTime - startTime;
            //listBox1.Items.Add("");
            //listBox1.Items.Add(string.Format("Time: {0}:{1}:{2}.{3}", difference.Hours, difference.Minutes, difference.Seconds, difference.Milliseconds));

            var list = ListCreator.CreateCardList();
            var diff = ListCreator.endTime - ListCreator.startTime;

            foreach (var c in list.OrderBy(x=>x.Id).ToList())
            {
                listBox1.Items.Add(string.Format("{0} -- {1}", c.Id, c.Title));
            }
            listBox1.Items.Add("");
            listBox1.Items.Add(string.Format("Time: {0}:{1}:{2}.{3}", diff.Hours, diff.Minutes, diff.Seconds, diff.Milliseconds));
        }

        private static string GetCardDetails(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = response.GetResponseStream();
                var result = response.CharacterSet == null
                    ? new StreamReader(stream).ReadToEnd()
                    : new StreamReader(stream, Encoding.GetEncoding(response.CharacterSet)).ReadToEnd();
                response.Close();
                stream.Close();

                string title = FilterPageForTitle(result);
                title = title.Remove(title.IndexOf("Gatherer"));
                return string.IsNullOrEmpty(title)
                    ? string.Empty
                    : title.Remove(title.Length - 3);
            }
            return string.Empty;
        }

        private static string FilterPageForTitle(string pageHtml)
        {
            var match = Regex.Match(pageHtml, @"<title>\s*(.+?)\s*</title>");
            return match.Success
                ? match.Groups[1].Value
                : string.Empty;
        }
    }
}
