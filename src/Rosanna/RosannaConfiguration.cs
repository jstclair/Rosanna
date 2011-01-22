using System;
using System.IO;
using Nancy.ViewEngines.Razor;

namespace Rosanna
{
    public class RosannaConfiguration : IRosannaConfiguration
    {
        public virtual Func<string, string, dynamic, Action<Stream>> ToHtml
        {
            get
            {
                return (path, view, model) => stream =>
                {
                    var result = new RazorViewEngine().RenderView(path + view + ".cshtml", model);
                    result.Execute(stream);
                };
            }
        }

        public virtual Func<DateTime, string> DateFormat
        {
            get { return dateTime => dateTime.ToShortDateString(); }
        }

        public virtual string Author
        {
            get { return "f"; }
        }

        public virtual string Title
        {
            get { return ""; }
        }

        public virtual string Url
        {
            get { return ""; }
        }

        public virtual string Prefix
        {
            get { return ""; }
        }

        public virtual string Disqus
        {
            get { return ""; }
        }

        public virtual string ArticleExtension
        {
            get { return ".md"; }
        }

    }
}