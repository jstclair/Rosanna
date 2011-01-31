using System;
using System.IO;
using System.Net;
using System.Web.Hosting;
using Nancy;

namespace Rosanna
{
    public class StaticFileResponse : Response
        {
            public static string GetFilePath(string filePath)
            {
                return HostingEnvironment.IsHosted ?
                    HostingEnvironment.MapPath(filePath) : filePath;
            }

            public StaticFileResponse(string filePath)
            {
                InitializeGenericFileResonse(filePath, "application/octet-stream");
            }

            public StaticFileResponse(string filePath, string contentType)
            {
                InitializeGenericFileResonse(filePath, contentType);
            }

            private void InitializeGenericFileResonse(string filePath, string contentType)
            {
                if (string.IsNullOrEmpty(filePath) ||
                    !File.Exists(GetFilePath(filePath)) ||
                    !Path.HasExtension(filePath))
                {
                    this.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    this.Contents = GetFileContent(filePath);
                    this.ContentType = contentType;
                    this.StatusCode = HttpStatusCode.OK;
                }
            }

            private static Action<Stream> GetFileContent(string filePath)
            {
                return stream =>
                {
                    using (var file = File.OpenRead(GetFilePath(filePath)))
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
                };
            }
        }

}