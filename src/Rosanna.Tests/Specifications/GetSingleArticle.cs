using System.Net;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetSingleArticle : RosannaSpecification
    {
        public GetSingleArticle()
        {
            NavigateTo("/2010/08/05/new-blog-in-five-minutes");
        }

        [Fact]
        public void Status_code_is_ok()
        {
            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Article_view_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h1>Article</h1>");
        }

        [Fact]
        public void Title_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h2>New blog in five minutes</h2>");
        }

        [Fact]
        public void Date_is_rendered()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<h3>August 5th 2010</h3>");
        }
    }
}