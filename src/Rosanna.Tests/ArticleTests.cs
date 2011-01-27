﻿using Rosanna.Tests.Specifications;
using Rosanna.ViewModels;
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

            _article = new Article("Articles\\2010-08-05-new-blog-in-five-minutes.md", _config);
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
            _article.Path.ShouldEqual("/2010/08/05/new-blog-in-five-minutes/");
        }

        [Fact]
        public void Path_should_include_prefix_when_specified()
        {
            _config.Prefix = "blog";
            _article.Path.ShouldEqual("/blog/2010/08/05/new-blog-in-five-minutes/");
        }

        [Fact]
        public void Path_should_include_prefix_when_specified_with_leading_slash()
        {
            _config.Prefix = "/blog";
            _article.Path.ShouldEqual("/blog/2010/08/05/new-blog-in-five-minutes/");
        }

        [Fact]
        public void Path_should_include_prefix_when_specified_with_trailing_slash()
        {
            _config.Prefix = "blog/";
            _article.Path.ShouldEqual("/blog/2010/08/05/new-blog-in-five-minutes/");
        }

        [Fact]
        public void Path_should_include_prefix_when_specified_with_leading_and_trailing_slash()
        {
            _config.Prefix = "/blog/";
            _article.Path.ShouldEqual("/blog/2010/08/05/new-blog-in-five-minutes/");
        }

        [Fact]
        public void Permalink_should_return_the_full_url()
        {
            _config.Url = "http://example.com";
            _article.Permalink.ShouldEqual("http://example.com/2010/08/05/new-blog-in-five-minutes/");
        }

        [Fact]
        public void Permalink_should_return_the_full_url_when_url_is_specified_with_trailing_slash()
        {
            _config.Url = "http://example.com/";
            _article.Permalink.ShouldEqual("http://example.com/2010/08/05/new-blog-in-five-minutes/");
        }

        [Fact]
        public void Permalink_should_include_http_even_when_not_specified()
        {
            _config.Url = "example.com";
            _article.Permalink.ShouldEqual("http://example.com/2010/08/05/new-blog-in-five-minutes/");
        }

        [Fact]
        public void Body_is_rendered_using_markdown()
        {
            _article.Body.ShouldMatch(s => s.StartsWith("<p>As part of learning"));
            _article.Body.ShouldMatch(s => s.EndsWith("cost you five minutes.</p>\n"));
        }

        [Fact]
        public void Summary_returns_first_150_characters_of_the_body()
        {
            _article.Summary.ShouldEqual(_article.Body.Substring(0, 150));
        }
    }
}