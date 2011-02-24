using Nancy;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetArchiveByMonthSpecs : RosannaSpecification
    {
        public GetArchiveByMonthSpecs()
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

        [Fact]
        public void Title_of_first_article_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h2>New blog in five minutes</h2>");
        }

        [Fact]
        public void Date_of_first_article_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h3>August 5th 2010</h3>");
        }

        [Fact]
        public void Title_of_second_article_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h2>Rosanna</h2>");
        }

        [Fact]
        public void Date_of_second_article_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h3>August 4th 2010</h3>");
        }
    }
}