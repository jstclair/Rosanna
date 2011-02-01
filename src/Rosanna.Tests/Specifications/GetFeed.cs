using System.Net;
using Rosanna.Formatters;
using Xunit;

namespace Rosanna.Tests.Specifications
{
    public class GetFeed : RosannaSpecification
    {
        public GetFeed()
        {
            NavigateTo("/index.xml");
        }

        [Fact]
        public void Status_code_is_ok()
        {
            Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Response_is_atom_response()
        {
            Response.ShouldBeOfType<AtomResponse>();
        }

        [Fact]
        public void Content_type_is_application_atom_xml()
        {
            Response.ContentType.ShouldEqual("application/atom+xml");
        }

        [Fact]
        public void Response_contains_atom_feed()
        {
            Response.GetStringContentsFromResponse().ShouldContain("<feed xmlns=\"http://www.w3.org/2005/Atom\">");
        }
    }
}