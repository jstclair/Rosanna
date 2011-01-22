using Nancy;
using Rosanna.ViewModels;

namespace Rosanna
{
    public class RosannaServer : NancyModule
    {
        public RosannaServer(IRosannaConfiguration configuration)
            : base(configuration.Prefix)
        {
            Get["/"] = x =>
                       {
                           return configuration.ToHtml("~/views/", "index", new IndexModel(configuration));
                       };
        }
    }
}
