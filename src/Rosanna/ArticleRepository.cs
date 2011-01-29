using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rosanna.ViewModels;

namespace Rosanna
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IRosannaConfiguration _config;

        public ArticleRepository(IRosannaConfiguration config)
        {
            _config = config;
        }

        public Article GetArticle(int year, int month, int day, string slug)
        {
            string filename = string.Format("Articles/{0}-{1:00}-{2:00}-{3}{4}", year, month, day, slug, _config.ArticleExtension);

            if (!File.Exists(filename))
                return null;

            return new Article(filename, _config);
        }

        public IEnumerable<Article> GetArticles(int year = 0, int month = 0, int day = 0)
        {
            string searchPattern = CreateSearchPattern(year, month, day);

            var articles = from file in Directory.EnumerateFiles("Articles/", searchPattern, SearchOption.TopDirectoryOnly)
                           orderby file descending
                           select new Article(file, _config);

            return articles;
        }

        private string CreateSearchPattern(int year, int month, int day)
        {
            return string.Format("{0}-{1:00}-{2:00}-*{3}", year, month, day, _config.ArticleExtension).Replace("00", "*").Replace("0", "*");
        }
    }
}