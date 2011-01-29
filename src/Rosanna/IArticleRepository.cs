using System.Collections.Generic;
using Rosanna.ViewModels;

namespace Rosanna
{
    public interface IArticleRepository
    {
        Article GetArticle(string year, string month, string day, string slug);
        IEnumerable<Article> GetArticles(string year = "*", string month = "*", string day = "*");
    }
}