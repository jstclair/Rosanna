using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using Rosanna.ViewModels;

namespace Rosanna
{
    public class FeedBuilder
    {
        private readonly IRosannaConfiguration _config;

        public FeedBuilder(IRosannaConfiguration config)
        {
            _config = config;
        }

        public virtual SyndicationFeed GetFeed(IEnumerable<Article> articles)
        {
            SyndicationFeed feed = CreateFeed(articles.First().GetDate());
            feed.Authors.Add(new SyndicationPerson { Name = _config.Author });
            feed.Items = CreateItems(articles.Take(10));

            return feed;
        }

        protected virtual SyndicationFeed CreateFeed(DateTime updated)
        {
            return new SyndicationFeed(_config.Title, null, null, _config.Url, updated);
        }

        protected virtual IEnumerable<SyndicationItem> CreateItems(IEnumerable<Article> articles)
        {
            return articles.Select(CreateItem);
        }

        protected virtual SyndicationItem CreateItem(Article article)
        {
            var item = new SyndicationItem
                       {
                           Id = article.Permalink,
                           PublishDate = article.GetDate(),
                           LastUpdatedTime = article.GetDate(), 
                           Title = new TextSyndicationContent(article.Title), 
                           Content = new TextSyndicationContent(article.Body, TextSyndicationContentKind.Html), 
                           Summary = new TextSyndicationContent(article.Summary, TextSyndicationContentKind.Html)
                       };

            item.Links.Add(new SyndicationLink(new Uri(article.Permalink)));
            item.Authors.Add(new SyndicationPerson { Name = article.Author });

            return item;
        }
    }
}