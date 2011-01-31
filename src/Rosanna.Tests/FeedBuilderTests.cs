using System;
using System.Linq;
using System.ServiceModel.Syndication;
using Xunit;

namespace Rosanna.Tests
{
    public class FeedBuilderTests
    {
        private readonly TestConfiguration _config;
        private readonly FeedBuilder _builder;
        private readonly SyndicationFeed _feed;

        public FeedBuilderTests()
        {
            _config = new TestConfiguration();
            _builder = new FeedBuilder(_config);
            
            _config.Title = "Rosanna";
            _config.Url = "http://example.com";
            _config.Author = "Author";

            _feed = _builder.GetFeed(new ArticleRepository(_config, new PathResolver(_config)).GetArticles());
        }

        [Fact]
        public void Reads_feed_title_from_config()
        {
            _feed.Title.Text.ShouldEqual("Rosanna");
        }

        [Fact]
        public void Reads_feed_id_from_config()
        {
            _feed.Id.ShouldEqual("http://example.com");
        }

        [Fact]
        public void Reads_author_from_config()
        {
            _feed.Authors[0].Name.ShouldEqual("Author");
        }

        [Fact]
        public void Reads_updated_from_the_last_updated_article()
        {
            _feed.LastUpdatedTime.ShouldEqual(new DateTimeOffset(new DateTime(2010, 08, 05)));
        }

        [Fact]
        public void Adds_an_entry_for_each_article()
        {
            _feed.Items.ShouldHaveCount(4);
        }

        [Fact]
        public void Entry_contains_data_from_article()
        {
            var item = _feed.Items.First();

            item.Id.ShouldEqual("http://example.com/web/2010/08/05/new-blog-in-five-minutes");
            item.Title.Text.ShouldEqual("New blog in five minutes");
            item.Links[0].Uri.OriginalString.ShouldEqual("http://example.com/web/2010/08/05/new-blog-in-five-minutes");
            item.LastUpdatedTime.ShouldEqual(new DateTimeOffset(new DateTime(2010, 08, 05)));
            item.PublishDate.ShouldEqual(new DateTimeOffset(new DateTime(2010, 08, 05)));
            item.Authors[0].Name.ShouldEqual("Author");
            item.Summary.Text.ShouldContain("<p>As part of learning Ruby");
            item.Summary.Type.ShouldEqual("html");
            item.Content.Type.ShouldEqual("html");
        }
    }
}