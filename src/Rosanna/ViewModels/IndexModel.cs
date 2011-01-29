using System.Collections.Generic;

namespace Rosanna.ViewModels
{
    public class IndexModel : BaseModel
    {
        public IEnumerable<Article> Articles { get; set; }

        public IndexModel(IRosannaConfiguration config, IEnumerable<Article> articles) : base(config)
        {
            Articles = articles;
        }
    }
}