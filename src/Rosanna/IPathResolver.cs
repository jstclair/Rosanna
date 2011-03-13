namespace Rosanna
{
    public interface IPathResolver
    {
        string GetMappedPath(string virtualPath);
        string GetVirtualPath(string virtualPath);
    }
}