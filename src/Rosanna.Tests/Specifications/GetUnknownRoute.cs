using System.Net;
using Xunit.Extensions;

namespace Rosanna.Tests.Specifications
{
    public class GetUnknownRoute : RosannaSpecification
    {
        [Theory]
        [InlineData("/unknown")]
        [InlineData("/2010/foo")]
        [InlineData("/unknown/1/bar")]
        [InlineData("/2010/08/05/i-do-not-exist")]
        public void Should_return_not_found_when_navigating_to_unknown_route(string route)
        {
            NavigateTo(route);

            Response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }
    }
}