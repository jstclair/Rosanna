using System.Net;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetAbout : RosannaSpecification
    {
        public GetAbout()
        {
            NavigateTo("/about");
        }

        [Fact]
        public void Status_code_is_ok()
        {
            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Index_view_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h1>About</h1>");
        }
    }
}