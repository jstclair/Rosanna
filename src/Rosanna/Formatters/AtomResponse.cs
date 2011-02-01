using System;
using System.IO;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Nancy;

namespace Rosanna.Formatters
{
    public class AtomResponse : Response
    {
        public AtomResponse(SyndicationFeed feed)
        {
            Contents = GetXmlContents(feed);
            ContentType = "application/atom+xml";
            StatusCode = HttpStatusCode.OK;
        }

        private static Action<Stream> GetXmlContents(SyndicationFeed feed)
        {
            return stream =>
            {
                var writer = new XmlTextWriter(stream, Encoding.UTF8);
                var atomFormatter = new Atom10FeedFormatter(feed);
                atomFormatter.WriteTo(writer);
            };
        }
    }
}