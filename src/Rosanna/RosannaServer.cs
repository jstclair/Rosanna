using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Nancy;
using Rosanna.Formatters;
using Rosanna.ViewModels;

namespace Rosanna
{
    public class RosannaServer : NancyModule
    {
        private readonly IRosannaConfiguration _config;
        private readonly IArticleRepository _articleRepository;
        private readonly IFeedBuilder _feedBuilder;
        private readonly IPathResolver _pathResolver;

        public RosannaServer(IRosannaConfiguration config, IArticleRepository articleRepository, IFeedBuilder feedBuilder, IPathResolver pathResolver)
            : base(config.Prefix)
        {
            _config = config;
            _articleRepository = articleRepository;
            _feedBuilder = feedBuilder;
            _pathResolver = pathResolver;

            DefineRoutes();
            DefineStaticContentRoutes();
        }

        private void DefineRoutes()
        {
            Get["/"] = x => GetIndex();
            Get["/archive"] = x => GetArchive();
            Get["/index.xml"] = x => GetFeed();
            Get["/{year}/{month}/{day}/{slug}"] = x => GetArticle(x.year, x.month, x.day, x.slug);
            Get["/{year}/{month}/{day}"] = x => GetArchive(x.year, x.month, x.day);
            Get["/{year}/{month}"] = x => GetArchive(x.year, x.month);
            Get["/{year}"] = x => GetArchive(x.year);
        }

        private void DefineStaticContentRoutes()
        {
            foreach (var path in _config.StaticContent)
            {
                string path1 = path;
                Get[path + "/{filename}"] = x => new StaticFileResponse(_pathResolver.GetMappedPath(path1 + "/" + x.filename));
            }
        }

        private AtomResponse GetFeed()
        {
            var feed = _feedBuilder.GetFeed(_articleRepository.GetArticles());

            return new AtomResponse(feed);
        }

        private Response GetIndex()
        {
            var articles = _articleRepository.GetArticles();

            return CreateResponse("index", new IndexModel(_config, articles));
        }

        private Response GetArticle(string year, string month, string day, string slug)
        {
            Article article = _articleRepository.GetArticle(year, month, day, slug);
            if (article == null)
                return NotFound();

            return CreateResponse("article", new ArticleModel(_config, article));
        }

        private Response GetArchive(string year = "*", string month = "*", string day = "*")
        {
            IEnumerable<Article> articles = Enumerable.Empty<Article>();

            if (IsDate(year, month, day))
            {
                articles = _articleRepository.GetArticles(year, month, day);
            }
            else if(IsMeta(year, month, day))
            {
                articles = _articleRepository.GetArticlesByMeta(year, month);                
            }
            else
            {
                return NotFound();
            }

            return CreateResponse("archive", new ArchiveModel(_config, year, month, day, articles));
        }

        private Response CreateResponse(string view, dynamic model)
        {
            Response response = _config.ToHtml(_pathResolver.GetVirtualPath("Views"), view, model);

            SetCacheControl(response);

            return response;
        }

        private static NotFoundResponse NotFound()
        {
            return new NotFoundResponse();
        }

        private void SetCacheControl(Response response)
        {
            var cache = string.Format("public, max-age={0}", _config.CacheAge);
            var noCache = "no-cache, must-revalidate";
            response.Headers.Add("Cache-Control", _config.CacheAge > 0 ? cache : noCache);
        }

        private static bool IsDate(string year, string month, string day)
        {
            return Regex.IsMatch(year, @"\d{4}|\*") && Regex.IsMatch(month, @"\d{2}|\*") && Regex.IsMatch(day, @"\d{2}|\*");
        }

        private static bool IsMeta(string year, string month, string day)
        {
            return Regex.IsMatch(year, @"[a-zA-Z]+") && Regex.IsMatch(month, @"[a-zA-Z]+") && Regex.IsMatch(day, @"\*");
        }
    }
}
