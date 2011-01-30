using System.Collections.Generic;
using System.ServiceModel.Syndication;
using Rosanna.ViewModels;

namespace Rosanna
{
    public interface IFeedBuilder
    {
        SyndicationFeed GetFeed(IEnumerable<Article> articles);
    }
}