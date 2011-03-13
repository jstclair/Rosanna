using System;

namespace Rosanna
{
    public interface IRosannaConfiguration
    {
        Func<DateTime, string> DateFormat { get; set; }
        string Author { get; set; }
        string Title { get; set; }
        string Url { get; set; }
        string Prefix { get; set; }
        string Disqus { get; set; }
        string ArticleExtension { get; set; }
        string SummaryDelimiter { get; set; }
        int CacheAge { get; set; }
    }
}