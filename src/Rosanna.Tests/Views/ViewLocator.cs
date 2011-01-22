using System;
using System.IO;
using Nancy.ViewEngines;

namespace Rosanna.Tests.Views
{
    public class ViewLocator : IViewLocator
    {
        public ViewLocationResult GetTemplateContents(string viewTemplate)
        {
            string fileName = Path.GetFileName(viewTemplate);
            var path = "Views\\" + fileName;

            return new ViewLocationResult(path, new StreamReader(path));
        }
    }
}