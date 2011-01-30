using System.Text.RegularExpressions;

namespace Rosanna.ViewModels
{
    public class ArticleSummary
    {
        private readonly string _body;
        private readonly IRosannaConfiguration _config;

        public ArticleSummary(string body, IRosannaConfiguration config)
        {
            _body = body;
            _config = config;
        }

        public static implicit operator string(ArticleSummary articleSummary)
        {
            var body = articleSummary._body;
            var config = articleSummary._config;
            string summary;

            if (body.Contains(config.SummaryDelimiter))
            {
                summary =  body.Substring(0, body.IndexOf(config.SummaryDelimiter));
            }
            else
            {
                summary =  Regex.Match(body, @"(.*?)(\n|\Z)", RegexOptions.Singleline).ToString();
            }

            if (summary.Length < body.Length)
                summary = Regex.Replace(summary, @"\.\Z", "&hellip;");

            return summary.TransformMarkdown();
        }

        public override string ToString()
        {
            return this;
        }
    }
}