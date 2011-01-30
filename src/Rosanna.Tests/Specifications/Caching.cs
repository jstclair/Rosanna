using Xunit.Extensions;

namespace Rosanna.Tests.Specifications
{
    public class Caching : RosannaSpecification
    {
        [Theory]
        [InlineData("/", 28800)]
        [InlineData("/archive", 28800)]
        [InlineData("/2010/08/05/new-blog-in-five-minutes", 28800)]
        [InlineData("/2010/08/05", 14400)]
        [InlineData("/2010/08", 14400)]
        [InlineData("/2010/", 14400)]
        public void Should_add_cache_control_with_max_age_to_header_when_specified_in_config(string path, int cacheAge)
        {
            Config.CacheAge = cacheAge;
            
            NavigateTo(path);

            Response.Headers["Cache-Control"].ShouldEqual(string.Format("public, max-age={0}", cacheAge));
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/archive")]
        [InlineData("/2010/08/05/new-blog-in-five-minutes")]
        [InlineData("/2010/08/05")]
        [InlineData("/2010/08")]
        [InlineData("/2010/")]
        public void Should_add_cache_control_with_no_cache_to_header_when_0_specified_in_config(string path)
        {
            Config.CacheAge = 0;
            
            NavigateTo(path);

            Response.Headers["Cache-Control"].ShouldEqual("no-cache, must-revalidate");
        }
    }
}