using Nancy;

namespace Rosanna.Tests.Specifications
{
    public class RosannaSpecification
    {
        public static INancyEngine Engine;
        public static Response Response;

        protected RosannaSpecification()
        {
            Engine = new RosannaBootstrapper().GetEngine();
        }

        protected void NavigateTo(string route)
        {
            Response = Engine.HandleRequest(new Request("GET", route, "http"));
        }
    }
}