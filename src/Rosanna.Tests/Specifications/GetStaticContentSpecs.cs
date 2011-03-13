using Nancy;
using Should;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetStaticContentSpecs : RosannaSpecification
    {
        [Fact]
        public void Can_get_static_javascript()
        {
            NavigateTo("/scripts/Application.js");

            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            Response.ContentType.ShouldEqual("text/javascript");
        }
        
        [Fact]
        public void Can_get_static_css()
        {
            NavigateTo("/styles/Application.css");

            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            Response.ContentType.ShouldEqual("text/css");
        }
    }
}