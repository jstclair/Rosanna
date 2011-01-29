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

        private Response GetArticle(int year, int month, int day, string slug)
        {
            Article article = _articleRepository.GetArticle(year, month, day, slug);
            if (article == null)
                return new NotFoundResponse();

            return CreateResponse("article", new ArticleModel(_config, article));
        }

        private Response GetArchive()
        {
            return GetArchive(new ArchiveModel(_config, "Archive"));
        }

        private Response GetArchiveByDay(int year, int month, int day)
        {
            return GetArchive(new ArchiveModel(_config, Request.Uri));
        }

        private Response GetArchiveByMonth(int year, int month)
        {
            return GetArchive(new ArchiveModel(_config, Request.Uri));
        }

        private Response GetArchiveByYear(int year)
        {
            return GetArchive(new ArchiveModel(_config, Request.Uri));
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
