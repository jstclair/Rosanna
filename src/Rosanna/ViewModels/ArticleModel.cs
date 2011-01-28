namespace Rosanna.ViewModels
{
    public class ArticleModel : BaseModel
    {
        public Article Article { get; set; }

        public ArticleModel(IRosannaConfiguration config, Article article) : base(config)
        {
            Article = article;
        }
    }
}