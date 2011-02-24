using System.Collections.Generic;
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
            SetCacheControl();
        }

        private void DefineRoutes()
        {
            Get["/"] = x => GetIndex();
            Get["/archive"] = x => GetArchive();
            Get["/about"] = x => GetAbout();
            Get["/index.xml"] = x => GetFeed();
            Get[@"/(?<year>\d{4})/(?<month>\d{2})/(?<day>\d{2})/{slug}"] = x => GetArticle(x.year, x.month, x.day, x.slug);
            Get[@"/(?<year>\d{4})/(?<month>\d{2})/(?<day>\d{2})"] = x => GetArchive(x.year, x.month, x.day);
            Get[@"/(?<year>\d{4})/(?<month>\d{2})"] = x => GetArchive(x.year, x.month);
            Get[@"/(?<year>\d{4})"] = x => GetArchive(x.year);
            Get[@"/(?<key>[A-Z]+)/(?<value>\w+)"] = x => GetArchiveByMeta(x.key, x.value);
        }

        private void DefineStaticContentRoutes()
        {
            foreach (var path in _config.StaticContent)
            {
                string path1 = path;
                Get[path + "/{filename}"] = x => new StaticFileResponse(_pathResolver.GetMappedPath(path1 + "/" + x.filename));
            }
        }

        private void SetCacheControl()
        {
            After += context =>
            {
                var cache = string.Format("public, max-age={0}", _config.CacheAge);
                var noCache = "no-cache, must-revalidate";
                context.Response.Headers.Add("Cache-Control", _config.CacheAge > 0 ? cache : noCache);
            };
        }

        private AtomResponse GetFeed()
        {
            var feed = _feedBuilder.GetFeed(_articleRepository.GetArticles());

            return new AtomResponse(feed);
        }

        private Response GetAbout()
        {
            return View[new AboutModel(_config)];
        }

        private Response GetIndex()
        {
            var articles = _articleRepository.GetArticles();

            return View[new IndexModel(_config, articles)];
        }

        private Response GetArticle(string year, string month, string day, string slug)
        {
            Article article = _articleRepository.GetArticle(year, month, day, slug);
            if (article == null)
                return new NotFoundResponse();

            return View[new ArticleModel(_config, article)];
        }

        private Response GetArchive(string year = "*", string month = "*", string day = "*")
        {
            IEnumerable<Article> articles = _articleRepository.GetArticles(year, month, day);

            return View[new ArchiveModel(_config, year, month, day, articles)];
        }

        private Response GetArchiveByMeta(string key, string value)
        {
            var articles = _articleRepository.GetArticlesByMeta(key, value);

            return View[new ArchiveModel(_config, key, value, articles)];
        }
    }
}
