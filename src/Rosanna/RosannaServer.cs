using System.Collections.Generic;
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
            const string byDay = @".\d{4}/\d{2}/\d{2}/?$";
            const string byMonth = @".\d{4}/\d{2}/?$";
            const string byYear = @".\d{4}/?$";
            const string byMeta = @".[A-Z]/\w+/?$";

            Get["/"] = x => GetIndex();
            Get["/archive"] = x => GetArchive();
            Get["/about"] = x => GetAbout();
            Get["/index.xml"] = x => GetFeed();
            Get["/{year}/{month}/{day}/{slug}"] = x => GetArticle(x.year, x.month, x.day, x.slug);
            Get["/{year}/{month}/{day}", r => Matches(r.Uri, byDay)] = x => GetArchive(x.year, x.month, x.day);
            Get["/{year}/{month}", r => Matches(r.Uri, byMonth)] = x => GetArchive(x.year, x.month);
            Get["/{year}", r => Matches(r.Uri, byYear)] = x => GetArchive(x.year);
            Get["/{key}/{value}", r => Matches(r.Uri, byMeta)] = x => GetArchiveByMeta(x.key, x.value);
        }

        private void DefineStaticContentRoutes()
        {
            foreach (var path in _config.StaticContent)
            {
                string path1 = path;
                Get[path + "/{filename}"] = x => new StaticFileResponse(_pathResolver.GetMappedPath(path1 + "/" + x.filename));
            }
        }

        private static bool Matches(string uri, string pattern)
        {
            return Regex.IsMatch(uri, pattern, RegexOptions.IgnoreCase);
        }

        private AtomResponse GetFeed()
        {
            var feed = _feedBuilder.GetFeed(_articleRepository.GetArticles());

            return new AtomResponse(feed);
        }

        private Response GetAbout()
        {
            return CreateResponse("about", new AboutModel(_config));
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
                return new NotFoundResponse();

            return CreateResponse("article", new ArticleModel(_config, article));
        }

        private Response GetArchive(string year = "*", string month = "*", string day = "*")
        {
            IEnumerable<Article> articles = _articleRepository.GetArticles(year, month, day);

            return CreateResponse("archive", new ArchiveModel(_config, year, month, day, articles));
        }

        private Response GetArchiveByMeta(string key, string value)
        {
            var articles = _articleRepository.GetArticlesByMeta(key, value);

            return CreateResponse("archive", new ArchiveModel(_config, key, value, articles));
        }

        private Response CreateResponse(string view, dynamic model)
        {
            Response response = _config.ToHtml(_pathResolver.GetVirtualPath("Views"), view, model);

            SetCacheControl(response);

            return response;
        }

        private void SetCacheControl(Response response)
        {
            var cache = string.Format("public, max-age={0}", _config.CacheAge);
            var noCache = "no-cache, must-revalidate";
            response.Headers.Add("Cache-Control", _config.CacheAge > 0 ? cache : noCache);
        }
    }
}
