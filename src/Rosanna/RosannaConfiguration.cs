using System;

namespace Rosanna
{
    public class RosannaConfiguration : IRosannaConfiguration
    {
        public RosannaConfiguration()
        {
            DateFormat = date => string.Format("{0:MMMM} {1} {0:yyyy}", date, date.Day.ToOrdinal());
            ArticleExtension = ".md";
            SummaryDelimiter = "~\n";
        }

        public Func<DateTime, string> DateFormat { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Prefix { get; set; }

        public string Disqus { get; set; }

        public string ArticleExtension { get; set; }

        public string SummaryDelimiter { get; set; }

        public int CacheAge { get; set; }
    }
}