using Nancy;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetAboutSpecs : RosannaSpecification
    {
        public GetAboutSpecs()
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