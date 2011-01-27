namespace Rosanna.ViewModels
{
    public class ArchiveModel : BaseModel
    {
        public string Path { get; set; }

        public ArchiveModel(IRosannaConfiguration config, string path) : base(config)
        {
            Path = path.TrimStart('/').TrimEnd('/');
        }
    }
}