using System;
using System.IO;
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
                return HostingEnvironment.MapPath("~/" + path);

            return path;
        }

        public string GetVirtualPath(string virtualPath)
        {
            string path = GetPath(virtualPath);

            if(HostingEnvironment.IsHosted)
                return "~/" + path;

            return path;
        }

        private string GetPath(string virtualPath)
        {
            if (virtualPath.StartsWith("~"))
                virtualPath = virtualPath.Substring(1);

            return Path.Combine(_config.Prefix ?? "", virtualPath) + "/";
        }
    }
}