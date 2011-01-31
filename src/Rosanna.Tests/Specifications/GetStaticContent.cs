using System.Net;
using Xunit.Extensions;

namespace Rosanna.Tests.Specifications
{
    public class GetStaticContent : RosannaSpecification
    {

        [Theory]
        [InlineData("/Scripts/Application.js")]
        [InlineData("/Styles/Application.css")]
        public void Can_get_static_content(string path)
        {
            Config.StaticContent = new[] {"/Scripts", "/Styles"};

            NavigateTo(path);

            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }
    }
}