namespace Rosanna.ViewModels
{
    public class BaseModel
    {
        private readonly IRosannaConfiguration _config;

        public BaseModel(IRosannaConfiguration config)
        {
            _config = config;
        }

        public string Author
        {
            get { return _config.Author; }
        }

        public string Title
        {
            get { return _config.Title; }
        }

        public string Url
        {
            get { return _config.Url; }
        }

        public string Prefix
        {
            get { return _config.Prefix; }
        }

        public string Disqus
        {
            get { return _config.Disqus; }
        }
    }
}