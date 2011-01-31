using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rosanna.ViewModels;

namespace Rosanna
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IRosannaConfiguration _config;
        private readonly IPathResolver _pathResolver;

        public ArticleRepository(IRosannaConfiguration config, IPathResolver pathResolver)
        {
            _config = config;
            _pathResolver = pathResolver;
        }

        public Article GetArticle(string year, string month, string day, string slug)
        {
            string filename = Path.Combine(_pathResolver.GetMappedPath("Articles"), CreateSearchPattern(year, month, day, slug));

            if (!File.Exists(filename))
                return null;

            return new Article(filename, _config);
        }

        public IEnumerable<Article> GetArticles(string year = "*", string month = "*", string day = "*")
        {
            string searchPattern = CreateSearchPattern(year, month, day);

            var articles = from file in Directory.EnumerateFiles(_pathResolver.GetMappedPath("Articles"), searchPattern, SearchOption.TopDirectoryOnly)
                           orderby file descending
                           select new Article(file, _config);

            return articles;
        }

        private string CreateSearchPattern(string year, string month, string day, string slug = "*")
        {
            return string.Format("{0}-{1}-{2}-{3}{4}", year, month, day, slug, _config.ArticleExtension);
        }
    }
}