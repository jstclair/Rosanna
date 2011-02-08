using System;
using System.IO;
using System.Net;
using Nancy;

namespace Rosanna.Formatters
{
    public class StaticFileResponse : Response
    {
        public StaticFileResponse(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            InitializeGenericFileResonse(filePath, MimeTypes.FromExtension(extension));
        }

        public StaticFileResponse(string filePath, string contentType)
        {
            InitializeGenericFileResonse(filePath, contentType);
        }

        private void InitializeGenericFileResonse(string filePath, string contentType)
        {
            if (FileExists(filePath))
            {
                Contents = GetFileContent(filePath);
                ContentType = contentType;
                StatusCode = HttpStatusCode.OK;
            }
            else
            {
                StatusCode = HttpStatusCode.NotFound;
            }
        }

        private static Action<Stream> GetFileContent(string filePath)
        {
            return stream =>
            {
                using (var file = File.OpenRead(filePath))
                {
                    var buffer = new byte[4096];
                    var read = 0;
                    while (read <= file.Length)
                    {
                        file.Read(buffer, 0, buffer.Length);
                        stream.Write(buffer, 0, buffer.Length);
                        read += buffer.Length;
                    }
                }
                //using (var reader = new StreamReader(filePath))
                //{
                //    using (var writer = new StreamWriter(stream))
                //    {
                //        writer.Write(reader.ReadToEnd());
                //        writer.Flush();
                //    }
                //}
            };
        }

        private static bool FileExists(string filePath)
        {
            return (!string.IsNullOrEmpty(filePath) && File.Exists(filePath)) && Path.HasExtension(filePath);
        }
    }

}