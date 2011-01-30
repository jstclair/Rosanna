using System.ServiceModel.Syndication;
using Nancy;
using Rosanna.ViewModels;

namespace Rosanna
{
    public class RosannaServer : NancyModule
    {
        private readonly IRosannaConfiguration _config;
        private readonly IArticleRepository _articleRepository;

        public RosannaServer(IRosannaConfiguration config, IArticleRepository articleRepository)
            : base(config.Prefix)
        {
            _config = config;
            _articleRepository = articleRepository;

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
            return new AtomResponse(new SyndicationFeed());
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
            var articles = _articleRepository.GetArticles(year, month, day);

            return CreateResponse("archive", new ArchiveModel(_config, Request.Uri, articles));
        }

        private Response CreateResponse(string view, dynamic model)
        {
            Response response = _config.ToHtml("~/views/", view, model);

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
