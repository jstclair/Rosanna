using System.Net;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetStaticContent : RosannaSpecification
    {
        public GetStaticContent()
        {
            Config.StaticContent = new[] { "/Scripts", "/Styles" };
        }

        [Fact]
        public void Can_get_static_javascript()
        {
            NavigateTo("/Scripts/Application.js");

            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            Response.ContentType.ShouldEqual("application/x-javascript");
        }
        
        [Fact]
        public void Can_get_static_css()
        {
            NavigateTo("/Styles/Application.css");

            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            Response.ContentType.ShouldEqual("text/css");
        }
    }
}