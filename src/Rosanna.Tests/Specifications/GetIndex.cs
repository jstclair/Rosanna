using System.Net;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetIndex : RosannaSpecification
    {
        public GetIndex()
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
            Response.GetStringContentsFromResponse().ShouldContain("<h1>Index</h1>");
        }
    }
}