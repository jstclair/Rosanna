using System.Collections.Generic;

namespace Rosanna.ViewModels
{
    public class ArchiveModel : BaseModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public string Path { get; set; }

        public ArchiveModel(IRosannaConfiguration config, string path, IEnumerable<Article> articles) 
            : base(config)
        {
            Articles = articles;
            Path = path.TrimStart('/').TrimEnd('/');
        }
    }
}