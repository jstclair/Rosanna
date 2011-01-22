using System;
using System.IO;

namespace Rosanna
{
    public interface IRosannaConfiguration
    {
        Func<string, string, dynamic, Action<Stream>> ToHtml { get; }
        Func<DateTime, string> DateFormat { get; }
        string Author { get; }
        string Title { get; }
        string Url { get; }
        string Prefix { get; }
        string Disqus { get; }
        string ArticleExtension { get; }
    }
}