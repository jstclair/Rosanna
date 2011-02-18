namespace Rosanna
{
    public interface IPathResolver
    {
        string GetMappedPath(string virtualPath);
    }
}