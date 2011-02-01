using System.Web.Hosting;

namespace Rosanna
{
    public class PathResolver : IPathResolver
    {
        private readonly IRosannaConfiguration _config;

        public PathResolver(IRosannaConfiguration config)
        {
            _config = config;
        }

        public string GetMappedPath(string virtualPath)
        {
            var path = GetPath(virtualPath);
            
            if (HostingEnvironment.IsHosted)
                path = HostingEnvironment.MapPath("~/" + path);
            
            return AppendTrailingSlash(path);
        }

        public string GetVirtualPath(string virtualPath)
        {
            string path = GetPath(virtualPath);

            if(HostingEnvironment.IsHosted)
                path = "~/" + path;
            
            return AppendTrailingSlash(path);
        }

        private string GetPath(string virtualPath)
        {
            if (virtualPath.StartsWith("~"))
                virtualPath = virtualPath.Substring(1);

            return string.Format("{0}/{1}", _config.Prefix, virtualPath).Squeeze("/").TrimStart('/');
        }

        private static string AppendTrailingSlash(string path)
        {
            if (path.Contains("."))
                return path;

            return path + "/";
        }
    }
}