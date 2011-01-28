using Rosanna.ViewModels;

namespace Rosanna
{
    public interface IArticleRepository
    {
        Article GetArticle(int year, int month, int day, string slug);
    }
}