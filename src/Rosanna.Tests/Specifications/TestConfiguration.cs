using System;
using System.IO;
using Nancy.ViewEngines.Razor;
using Rosanna.Tests.Views;

namespace Rosanna.Tests.Specifications
{
    public class TestConfiguration : RosannaConfiguration
    {
        public override Func<string, string, dynamic, Action<Stream>> ToHtml
        {
            get
            {
                return (path, view, model) => stream =>
                {
                    dynamic result = new RazorViewEngine(new ViewLocator()).RenderView(path + view + ".cshtml", model);
                    result.Execute(stream);
                };
            }
        }
    }
}