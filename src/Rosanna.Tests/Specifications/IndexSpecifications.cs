using System.Net;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class IndexSpecifications : RosannaSpecification
    {
        public IndexSpecifications()
        {
            NavigateTo("/");
        }

        [Fact]
        public void Status_code_is_ok()
        {
            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Index_view_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h1>Hello</h1>");
        }
    }
}