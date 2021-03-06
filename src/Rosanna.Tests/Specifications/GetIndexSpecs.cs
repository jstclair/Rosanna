using Nancy;
using Should;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetIndexSpecs : RosannaSpecification
    {
        public GetIndexSpecs()
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

        [Fact]
        public void Title_of_third_article_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h2>Nancy</h2>");
        }

        [Fact]
        public void Date_of_third_article_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h3>July 6th 2010</h3>");
        }

        [Fact]
        public void Title_of_forth_article_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h2>Toto</h2>");
        }

        [Fact]
        public void Date_of_forth_article_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h3>January 1st 2009</h3>");
        }
    }
}