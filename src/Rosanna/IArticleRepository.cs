using System.Collections.Generic;
using Rosanna.ViewModels;

namespace Rosanna
{
    public interface IArticleRepository
    {
        Article GetArticle(int year, int month, int day, string slug);
        IEnumerable<Article> GetArticles(int year = 0, int month = 0, int day = 0);
    }
}