using Rosanna.Tests.Specifications;
using Rosanna.ViewModels;
using Xunit;

namespace Rosanna.Tests
{
    public class ArticleRepositoryTests
    {
        private readonly IArticleRepository _repository;
       
        public ArticleRepositoryTests()
        {
            _repository = new ArticleRepository(new TestConfiguration());
        }

        [Fact]
        public void Can_get_article_by_year_month_day_and_slug()
        {
            Article article = _repository.GetArticle(2010, 08, 05, "new-blog-in-five-minutes");

            article.Title.ShouldEqual("New blog in five minutes");
            article.Slug.ShouldEqual("new-blog-in-five-minutes");
            article.Date.ShouldEqual("August 5th 2010");
        }

        [Fact]
        public void Returns_null_when_requested_article_does_not_exist()
        {
            Article article = _repository.GetArticle(2010, 08, 05, "i-do-not-exist");

            article.ShouldBeNull();
        }
    }
}