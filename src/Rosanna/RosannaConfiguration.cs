using System;
using System.IO;
using System.Web.Hosting;
using Nancy.ViewEngines;
using Nancy.ViewEngines.Razor;

namespace Rosanna
{
    public class RosannaConfiguration : IRosannaConfiguration
    {
        public RosannaConfiguration()
        {
            ToHtml = (path, view, model) => stream =>
            {
                var result = new RazorViewEngine(new AspNetTemplateLocator()).RenderView(path + view + ".cshtml", model);
                result.Execute(stream);
            };

            DateFormat = date => string.Format("{0:MMMM} {1} {0:yyyy}", date, date.Day.ToOrdinal());
            ArticleExtension = ".md";
            ArticlePath = HostingEnvironment.MapPath("~/Articles/");
            SummaryDelimiter = "~\n";
        }

        public Func<string, string, dynamic, Action<Stream>> ToHtml { get; set; }

        public Func<DateTime, string> DateFormat { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Prefix { get; set; }

        public string Disqus { get; set; }

        public string ArticleExtension { get; set; }

        public string ArticlePath { get; set; }

        public string SummaryDelimiter { get; set; }
    }
}