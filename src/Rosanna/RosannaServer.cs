using Nancy;
using Rosanna.ViewModels;

namespace Rosanna
{
    public class RosannaServer : NancyModule
    {
        public RosannaServer(IRosannaConfiguration config, IArticleRepository articleRepository)
            : base(config.Prefix)
        {
            Get["/"] = x =>
                       {
                           return config.ToHtml("~/views/", "index", new IndexModel(config));
                       };

            Get["/archive"] = x =>
                       {
                           return config.ToHtml("~/views/", "archive", new ArchiveModel(config, "Archive"));
                       };

            Get["/{year}/{month}/{day}/{slug}"] = x =>
                    {
                        Article article = articleRepository.GetArticle(x.year, x.month, x.day, x.slug);
                        return config.ToHtml("~/views/", "article", new ArticleModel(config, article));
                       };

            Get["/{year}/{month}/{day}"] = x =>
                       {
                           return config.ToHtml("~/views/", "archive", new ArchiveModel(config, Request.Uri));
                       };

            Get["/{year}/{month}"] = x =>
                       {
                           return config.ToHtml("~/views/", "archive", new ArchiveModel(config, Request.Uri));
                       };

            Get["/{year}"] = x =>
                       {
                           return config.ToHtml("~/views/", "archive", new ArchiveModel(config, Request.Uri));
                       };

        }
    }
}
