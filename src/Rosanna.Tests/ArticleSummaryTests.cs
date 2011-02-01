using Rosanna.ViewModels;
using Xunit;

namespace Rosanna.Tests
{
    public class ArticleSummaryTests
    {
        [Fact]
        public void Summary_is_specified_by_delimiter()
        {
            string summary = CreateSummary("Summary specified by ~\n delimiter.");

            summary.ShouldEqual("<p>Summary specified by </p>\n");
        }

        [Fact]
        public void Summary_includes_entire_article_when_only_one_paragraph()
        {
            var summary = CreateSummary("Summary");
            
            summary.ShouldEqual("<p>Summary</p>\n");
        }

        [Fact]
        public void Summary_includes_first_paragraph_when_more_than_one()
        {
            var summary = CreateSummary("Summary\nSecond paragraph");
            
            summary.ShouldEqual("<p>Summary</p>\n");
        }

        [Fact]
        public void Replaces_last_punctuation_with_three_when_more_than_one_paragraph()
        {
            string summary = CreateSummary("One. Summary.\nSecond paragraph");
            
            summary.ShouldEqual("<p>One. Summary&hellip;</p>\n");
        }

        [Fact]
        public void Does_not_Replace_last_punctuation_when_only_one_paragraph()
        {
            string summary = CreateSummary("One. Summary.");
            
            summary.ShouldEqual("<p>One. Summary.</p>\n");
        }

        private static string CreateSummary(string article)
        {
            return new ArticleSummary(article, new TestConfiguration());
        }
    }

}