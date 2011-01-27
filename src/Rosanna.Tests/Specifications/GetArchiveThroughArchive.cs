using System.Net;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetArchiveThroughArchive : RosannaSpecification
    {
        public GetArchiveThroughArchive()
        {
            NavigateTo("/archive");
        }

        [Fact]
        public void Status_code_is_ok()
        {
            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Archive_view_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h1>Archive</h1>");
        }
    }
}