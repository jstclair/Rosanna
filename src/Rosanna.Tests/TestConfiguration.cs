using Nancy.ViewEngines.Razor;
using Rosanna.Tests.Views;

namespace Rosanna.Tests
{
    public class TestConfiguration : RosannaConfiguration
    {
        public TestConfiguration()
        {
            ToHtml = (path, view, model) => stream =>
            {
                dynamic result = new RazorViewEngine(new ViewLocator()).RenderView(path + view + ".cshtml", model);
                result.Execute(stream);
            };

            Url = "http://example.com";
            Prefix = "web";
            ArticlePath = "Articles/";
            Author = "Author";
        }
    }
}