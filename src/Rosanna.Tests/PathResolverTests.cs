using Xunit;

namespace Rosanna.Tests
{
    public class PathResolverTests
    {
        private readonly IRosannaConfiguration _config;
        private readonly PathResolver _pathResolver;

        public PathResolverTests()
        {
            _config = new TestConfiguration();
            _pathResolver = new PathResolver(_config);
        }

        [Fact]
        public void Can_resolve_mapped_path_when_prefix_is_set()
        {
            _config.Prefix = "prefix";

            var mappedPath = _pathResolver.GetMappedPath("Views");

            mappedPath.ShouldEqual("prefix/Views/");
        }

        [Fact]
        public void Can_resolve_mapped_path_when_prefix_is_not_set()
        {
            _config.Prefix = null;

            var mappedPath = _pathResolver.GetMappedPath("Views");

            mappedPath.ShouldEqual("Views/");
        }

        [Fact]
        public void Can_resolve_mapped_file_path_when_prefix_is_set()
        {
            _config.Prefix = "prefix";

            var mappedPath = _pathResolver.GetMappedPath("js/app.js");

            mappedPath.ShouldEqual("prefix/js/app.js");
        }

        [Fact]
        public void Can_resolve_mapped_file_path_when_prefix_is_not_set()
        {
            _config.Prefix = null;

            var mappedPath = _pathResolver.GetMappedPath("js/app.js");

            mappedPath.ShouldEqual("js/app.js");
        }

        [Fact]
        public void Can_resolve_virtual_path_when_prefix_is_set()
        {
            _config.Prefix = "prefix";

            var mappedPath = _pathResolver.GetVirtualPath("Views");

            mappedPath.ShouldEqual("prefix/Views/");
        }

        [Fact]
        public void Can_resolve_virtual_path_when_prefix_is_not_set()
        {
            _config.Prefix = null;

            var mappedPath = _pathResolver.GetMappedPath("Views");

            mappedPath.ShouldEqual("Views/");
        }

        [Fact]
        public void Can_resolve_virtual_file_path_when_prefix_is_set()
        {
            _config.Prefix = "prefix";

            var mappedPath = _pathResolver.GetVirtualPath("js/app.js");

            mappedPath.ShouldEqual("prefix/js/app.js");
        }

        [Fact]
        public void Can_resolve_virtual_file_path_when_prefix_is_not_set()
        {
            _config.Prefix = null;

            var mappedPath = _pathResolver.GetVirtualPath("js/app.js");

            mappedPath.ShouldEqual("js/app.js");
        }
    }
}