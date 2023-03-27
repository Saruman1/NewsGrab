namespace NewsGrab
{
    using HtmlAgilityPack;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            // Load the TSN website
            var web = new HtmlWeb();
            var doc = web.Load("https://tsn.ua/");

            // Get the news headlines
            var headlines = doc.DocumentNode
                .Descendants("a")
                .Where(x => x.GetAttributeValue("class", "")
                    .Contains("c-card__link"))
               .Select(x => new { Title = x.InnerText.Trim().Replace("&quot;", "\""), Url = x.GetAttributeValue("href", "").Replace("&quot;", "\"") });


            using (var writer = new StreamWriter(@"YOUR PATH\news.html"))
            {
                writer.WriteLine("<html>");
                writer.WriteLine("<head>");
                writer.WriteLine("<title>News Headlines</title>");
                writer.WriteLine("</head>");
                writer.WriteLine("<body>");

                foreach (var headline in headlines)
                {
                    writer.WriteLine($"<a href=\"{headline.Url}\">{headline.Title}</a>");
                    writer.WriteLine();
                    writer.WriteLine("<p>");
                }

                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }

            Console.WriteLine("   Data stored successfully!");
            Console.ReadLine();
        }
    }

}
