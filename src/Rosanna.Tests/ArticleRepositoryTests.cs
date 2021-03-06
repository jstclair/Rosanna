using System.Collections.Generic;
using System.Linq;
using Rosanna.ViewModels;
using Should;
using Xunit;

namespace Rosanna.Tests
{
    public class ArticleRepositoryTests
    {
        private readonly IArticleRepository _repository;
       
        public ArticleRepositoryTests()
        {
            var config = new TestConfiguration();
            _repository = new ArticleRepository(config, new PathResolver(config));
        }

        [Fact]
        public void Can_get_article_by_year_month_day_and_slug()
        {
            Article article = _repository.GetArticle("2010", "08", "05", "new-blog-in-five-minutes");

            article.Title.ShouldEqual("New blog in five minutes");
            article.Slug.ShouldEqual("new-blog-in-five-minutes");
            article.Date.ShouldEqual("August 5th 2010");
        }

        [Fact]
        public void Returns_null_when_requested_article_does_not_exist()
        {
            Article article = _repository.GetArticle("2010", "08", "05", "i-do-not-exist");

            article.ShouldBeNull();
        }

        [Fact]
        public void Can_get_articles_by_day()
        {
            IEnumerable<Article> articles = _repository.GetArticles("2010", "08", "05");

            articles.Count().ShouldEqual(1);
            articles.First().Title.ShouldEqual("New blog in five minutes");
        }

        [Fact]
        public void Can_get_articles_by_month()
        {
            IEnumerable<Article> articles = _repository.GetArticles("2010", "08");

            articles.Count().ShouldEqual(2);
            articles.First().Title.ShouldEqual("New blog in five minutes");
            articles.Last().Title.ShouldEqual("Rosanna");
        }

        [Fact]
        public void Can_get_articles_by_year()
        {
            IEnumerable<Article> articles = _repository.GetArticles("2010");

            articles.Count().ShouldEqual(3);
            articles.ElementAt(0).Title.ShouldEqual("New blog in five minutes");
            articles.ElementAt(1).Title.ShouldEqual("Rosanna");
            articles.ElementAt(2).Title.ShouldEqual("Nancy");
        }

        [Fact]
        public void Can_get_all_articles()
        {
            IEnumerable<Article> articles = _repository.GetArticles();

            articles.Count().ShouldEqual(4);
            articles.ElementAt(0).Title.ShouldEqual("New blog in five minutes");
            articles.ElementAt(1).Title.ShouldEqual("Rosanna");
            articles.ElementAt(2).Title.ShouldEqual("Nancy");
            articles.ElementAt(3).Title.ShouldEqual("Toto");
        }

        [Fact]
        public void Can_get_articles_by_meta_data()
        {
            IEnumerable<Article> articles = _repository.GetArticlesByMeta("tags", "tag");

            articles.Count().ShouldEqual(1);
            articles.First().Title.ShouldEqual("Rosanna");
        }

        [Fact]
        public void Can_get_articles_by_meta_data_2()
        {
            IEnumerable<Article> articles = _repository.GetArticlesByMeta("author", "Rosanna");

            articles.Count().ShouldEqual(1);
            articles.First().Title.ShouldEqual("Rosanna");
        }
    }
}