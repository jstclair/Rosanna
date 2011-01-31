using System.Collections.Generic;

namespace Rosanna.ViewModels
{
    public class ArchiveModel : BaseModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public string Path { get; set; }

        public ArchiveModel(IRosannaConfiguration config, string year, string month, string day, IEnumerable<Article> articles) 
            : base(config)
        {
            Articles = articles;
            Path = GetPath(year, month, day);
        }

        private static string GetPath(string year, string month, string day)
        {
            if (year == "*" && month == "*" && day == "*")
                return "Archive";

            return string.Format("{0}/{1}/{2}", year, month, day).TrimEnd("/*".ToCharArray());
        }
    }
}