using System.Text.RegularExpressions;
using Nancy;
using Rosanna.ViewModels;

namespace Rosanna
{
    public class RosannaServer : NancyModule
    {
        private readonly IRosannaConfiguration _config;
        private readonly IArticleRepository _articleRepository;
        private readonly IFeedBuilder _feedBuilder;

        public RosannaServer(IRosannaConfiguration config, IArticleRepository articleRepository, IFeedBuilder feedBuilder)
            : base(config.Prefix)
        {
            _config = config;
            _articleRepository = articleRepository;
            _feedBuilder = feedBuilder;

            DefineRoutes();
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
            if (!ValidateParameters(year, month, day))
                return NotFound();

            var articles = _articleRepository.GetArticles(year, month, day);

            return CreateResponse("archive", new ArchiveModel(_config, Request.Uri, articles));
        }

        private Response CreateResponse(string view, dynamic model)
        {
            Response response = _config.ToHtml("~/views/", view, model);

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

        private static bool ValidateParameters(string year, string month, string day)
        {
            return Regex.IsMatch(year, @"\d{4}|\*") && Regex.IsMatch(month, @"\d{2}|\*") && Regex.IsMatch(day, @"\d{2}|\*");
        }
    }
}
