using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.HtmlHelpers
{
    public static class GathererGrab
    {
        public static string GetPageTitle(string url)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            var response = (HttpWebResponse) request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = response.GetResponseStream();
                var result = response.CharacterSet == null
                    ? new StreamReader(stream).ReadToEnd()
                    : new StreamReader(stream, Encoding.GetEncoding(response.CharacterSet)).ReadToEnd();
                response.Close();
                stream.Close();

                return FilterPageForTitle(result);
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
