using System.Net;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetArchiveByMonth : RosannaSpecification
    {
        public GetArchiveByMonth()
        {
            NavigateTo("/2010/08");
        }

        [Fact]
        public void Status_code_is_ok()
        {
            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Archive_view_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h1>2010/08</h1>");
        }
    }
}