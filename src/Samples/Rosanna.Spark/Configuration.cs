using Nancy.ViewEngines;
using Nancy.ViewEngines.Spark;

namespace Rosanna.Spark
{
    public class Configuration : RosannaConfiguration
    {
        public Configuration()
        {
            ToHtml = (path, view, model) => stream =>
            {
                var factory = new ViewFactory();
                ViewResult result = factory.RenderView(path + view + ".spark", model);
                result.Execute(stream);
            };

            Author = "Sparky";
            Url = "http://example.com";
            Title = "Rosanna and Spark";
        }
    }
}