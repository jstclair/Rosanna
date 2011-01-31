using Nancy;

namespace Rosanna.Tests.Specifications
{
    public class RosannaSpecification
    {
        public static INancyEngine Engine;
        public static Response Response;
        public static IRosannaConfiguration Config;

        public RosannaSpecification()
        {
            var bootstrapper = new RosannaBootstrapper();
            Engine = bootstrapper.GetEngine();
            Config = bootstrapper.Container.Resolve<IRosannaConfiguration>();
        }

        protected void NavigateTo(string route)
        {
            route = "/" + Config.Prefix + route;
            Response = Engine.HandleRequest(new Request("GET", route, "http"));
        }
    }
}