using Rosanna.ViewModels;
using Should;
using Xunit;

namespace Rosanna.Tests
{
    public class ArticleTests
    {
        private Article _article;
        private readonly TestConfiguration _config;

        public ArticleTests()
        {
            _config = new TestConfiguration();

            _article = new Article("Web\\Articles\\2010-08-05-new-blog-in-five-minutes.md", _config);
        }

        [Fact]
        public void Date_is_read_from_file_name_and_formatted_according_to_config()
        {
            _article.Date.ShouldEqual("August 5th 2010");
        }

        [Fact]
        public void Title_is_read_from_meta_data()
        {
            _article.Title.ShouldEqual("New blog in five minutes");
        }

        [Fact]
        public void Slug_is_created_from_title()
        {
            _article.Slug.ShouldEqual("new-blog-in-five-minutes");
        }

        [Fact]
        public void Path_is_created_from_date_and_slug()
        {
            _config.Prefix = null;
            _article.Path.ShouldEqual("/2010/08/05/new-blog-in-five-minutes");
        }

        [Fact]
        public void Path_should_include_prefix_when_specified()
        {
            _config.Prefix = "blog";
            _article.Path.ShouldEqual("/blog/2010/08/05/new-blog-in-five-minutes");
        }

        [Fact]
        public void Path_should_include_prefix_when_specified_with_leading_slash()
        {
            _config.Prefix = "/blog";
            _article.Path.ShouldEqual("/blog/2010/08/05/new-blog-in-five-minutes");
        }

        [Fact]
        public void Path_should_include_prefix_when_specified_with_trailing_slash()
        {
            _config.Prefix = "blog/";
            _article.Path.ShouldEqual("/blog/2010/08/05/new-blog-in-five-minutes");
        }

        [Fact]
        public void Path_should_include_prefix_when_specified_with_leading_and_trailing_slash()
        {
            _config.Prefix = "/blog/";
            _article.Path.ShouldEqual("/blog/2010/08/05/new-blog-in-five-minutes");
        }

        [Fact]
        public void Permalink_should_return_the_full_url()
        {
            _config.Url = "http://example.com";
            _article.Permalink.ShouldEqual("http://example.com/web/2010/08/05/new-blog-in-five-minutes");
        }

        [Fact]
        public void Permalink_should_return_the_full_url_when_url_is_specified_with_trailing_slash()
        {
            _config.Url = "http://example.com/";
            _article.Permalink.ShouldEqual("http://example.com/web/2010/08/05/new-blog-in-five-minutes");
        }

        [Fact]
        public void Permalink_should_include_http_even_when_not_specified()
        {
            _config.Url = "example.com";
            _article.Permalink.ShouldEqual("http://example.com/web/2010/08/05/new-blog-in-five-minutes");
        }

        [Fact]
        public void Body_is_rendered_using_markdown()
        {
            _article.Body.StartsWith("<p>As part of learning").ShouldBeTrue();
            _article.Body.EndsWith("cost you five minutes.</p>\n").ShouldBeTrue();
        }

        [Fact]
        public void Should_strip_out_the_summary_delimiter_from_the_body()
        {
            _article.Body.ShouldNotContain(_config.SummaryDelimiter);
        }

        [Fact]
        public void Author_returns_the_author_from_configuration()
        {
            _article.Author.ShouldEqual("Author");
        }

        [Fact]
        public void If_author_is_specified_in_atricle_it_overrides_the_one_from_config()
        {
            var articleWithAuthorSpecified = new Article("Web\\Articles\\2010-08-04-rosanna.md", _config);

            articleWithAuthorSpecified.Author.ShouldEqual("Rosanna");
        }
    }
}