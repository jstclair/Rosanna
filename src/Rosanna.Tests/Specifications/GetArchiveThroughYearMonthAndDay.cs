using System.Net;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetArchiveThroughYearMonthAndDay : RosannaSpecification
    {
        public GetArchiveThroughYearMonthAndDay()
        {
            NavigateTo("/2010/08/05");
        }

        [Fact]
        public void Status_code_is_ok()
        {
            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Archive_view_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h1>2010/08/05</h1>");
        }
    }
}