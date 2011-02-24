using System;
using System.Collections.Generic;
using System.IO;
using Nancy;
using Nancy.ViewEngines;
using Nancy.ViewEngines.Razor;

namespace Rosanna.Tests.Specifications
{
    public abstract class RosannaSpecification
    {
        public static INancyEngine Engine;
        public static Response Response;
        public static IRosannaConfiguration Config;

        protected RosannaSpecification()
        {
            var bootstrapper = new RosannaBootstrapper();
            bootstrapper.Initialise();
            Engine = bootstrapper.GetEngine();
            bootstrapper.Container.Register<IViewSourceProvider, FileSystemViewSourceProvider>();
            bootstrapper.Container.Register<IViewEngine, RazorViewEngine>();
            Config = bootstrapper.Container.Resolve<IRosannaConfiguration>();
        }

        protected void NavigateTo(string route)
        {
            route = "/" + Config.Prefix + route;
            Response = Engine.HandleRequest(new Request("GET", route, "http")).Response;
        }
    }

    public class FileSystemViewSourceProvider : IViewSourceProvider
    {
        public ViewLocationResult LocateView(string viewName, IEnumerable<string> supportedViewEngineExtensions)
        {
            var viewFolder = Path.Combine(Environment.CurrentDirectory, "web", "views");
            var viewFile = Path.Combine(viewFolder, viewName + ".cshtml");

            return new ViewLocationResult(viewFile, "cshtml", new StreamReader(viewFile));
        }
    }
}