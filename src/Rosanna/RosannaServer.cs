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
            Get["/{year}/{month}/{day}/{slug}"] = x => GetArticle(x.year, x.month, x.day, x.slug);
            Get["/{year}/{month}/{day}"] = x => GetArchiveByDay(x.year, x.month, x.day);
            Get["/{year}/{month}"] = x => GetArchiveByMonth(x.year, x.month);
            Get["/{year}"] = x => GetArchiveByYear(x.year);
        }

        private Response GetIndex()
        {
            return CreateResponse("index", new IndexModel(_config));
        }

        private Response GetArticle(string year, string month, string day, string slug)
        {
            Article article = _articleRepository.GetArticle(year, month, day, slug);
            if (article == null)
                return new NotFoundResponse();

            return CreateResponse("article", new ArticleModel(_config, article));
        }

        private Response GetArchive()
        {
            var articles = _articleRepository.GetArticles();

            return GetArchive(new ArchiveModel(_config, "Archive", articles));
        }

        private Response GetArchiveByDay(string year, string month, string day)
        {
            var articles = _articleRepository.GetArticles(year, month, day);

            return GetArchive(new ArchiveModel(_config, Request.Uri, articles));
        }

        private Response GetArchiveByMonth(string year, string month)
        {
            var articles = _articleRepository.GetArticles(year, month);
         
            return GetArchive(new ArchiveModel(_config, Request.Uri, articles));
        }

        private Response GetArchiveByYear(string year)
        {
            var articles = _articleRepository.GetArticles(year);

            return GetArchive(new ArchiveModel(_config, Request.Uri, articles));
        }

        private Response GetArchive(ArchiveModel archiveModel)
        {
            return CreateResponse("archive", archiveModel);
        }

        private Response CreateResponse(string view, dynamic model)
        {
            return _config.ToHtml("~/views/", view, model);
        }
    }
}
