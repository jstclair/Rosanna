using Nancy;
using System.IO;

namespace Rosanna.Tests
{
    public static class ResponseExtensions
    {
        public static string GetStringContentsFromResponse(this Response response)
        {
            var memory = new MemoryStream();
            response.Contents.Invoke(memory);
            memory.Position = 0;
            using (var reader = new StreamReader(memory))
            {
                return reader.ReadToEnd();
            }
        }
    }
}