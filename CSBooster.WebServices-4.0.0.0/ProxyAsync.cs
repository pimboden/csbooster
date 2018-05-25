// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using AJAXASMXHandler;

namespace _4screen.CSB.WebServices
{
    [WebService(Namespace = "http://www.4screen.ch/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class ProxyAsync : System.Web.Services.WebService
    {
        private const string CACHE_KEY = "ProxyAsync.";

        public ProxyAsync()
        {
        }

        private class GetStringState : AsyncWebMethodState
        {
            public HttpWebRequest Request;
            public string Url;
            public int CacheDuration;

            public GetStringState(object state)
                : base(state)
            {
            }
        }

        [ScriptMethod]
        public IAsyncResult BeginGetString(string url, int cacheDuration, AsyncCallback cb, object state)
        {
            // See if the response from the URL is already cached on server
            string cachedContent = Context.Cache[CACHE_KEY + url] as string;
            if (!string.IsNullOrEmpty(cachedContent))
            {
                CacheResponse(Context, cacheDuration);
                return new AsmxHandlerSyncResult(cachedContent);
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            GetStringState myState = new GetStringState(state);
            myState.Request = request;
            myState.Url = url;
            myState.CacheDuration = cacheDuration;

            return request.BeginGetResponse(cb, myState);
        }

        [ScriptMethod]
        public string EndGetString(IAsyncResult result)
        {
            GetStringState state = result.AsyncState as GetStringState;

            HttpWebRequest request = state.Request;
            using (HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string content = reader.ReadToEnd();
                    state.Context.Cache.Insert(state.Url, content, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(state.CacheDuration), CacheItemPriority.Normal, null);

                    // produce cache headers for response caching
                    CacheResponse(state.Context, state.CacheDuration);

                    return content;
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string GetString(string url, int cacheDuration)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetXml(string url, int cacheDuration)
        {
            return GetString(url, cacheDuration);
        }

        [ScriptMethod]
        public IAsyncResult BeginGetXml(string url, int cacheDuration, AsyncCallback cb, object state)
        {
            return BeginGetString(url, cacheDuration, cb, state);
        }

        [ScriptMethod]
        public string EndGetXml(IAsyncResult result)
        {
            return EndGetString(result);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public object GetRss(string url, int count, int cacheDuration)
        {
            var feed = Context.Cache[CACHE_KEY + url] as XElement;
            if (feed == null)
            {
                if ((string)Context.Cache[CACHE_KEY + url] == string.Empty)
                    return null;
                try
                {
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                    request.Timeout = 15000;
                    using (WebResponse response = request.GetResponse())
                    {
                        using (XmlTextReader reader = new XmlTextReader(response.GetResponseStream()))
                        {
                            feed = XElement.Load(reader);
                        }
                    }

                    if (feed == null)
                        return null;
                    Context.Cache.Insert(CACHE_KEY + url, feed, null, DateTime.MaxValue, TimeSpan.FromMinutes(15));
                }
                catch
                {
                    Context.Cache[CACHE_KEY + url] = string.Empty;
                    return null;
                }
            }

            XNamespace ns = "http://www.w3.org/2005/Atom";

            // see if RSS or Atom

            try
            {
                // TODO: Use WCF

                // RSS
                /*if (feed.Element("channel") != null)
                {
                    return (from item in feed.Element("channel").Elements("item")
                            select
                                new Rss_Item
                                {
                                    Title = item.Element("title").Value,
                                    Link = item.Element("link").Value,
                                    Description = item.Element("description").Value,
                                    PubDate = item.Element("pubDate") != null ? Rfc822DateTime.Parse(item.Element("pubDate").Value.Replace("\r\n","").Trim()) : DateTime.MinValue
                                }
                                 ).Take(count);
                    //DateTime dt = DateTime.
                }


                    // Atom
                else if (feed.Element(ns + "entry") != null)
                    return (from item in feed.Elements(ns + "entry") 
                            select 
                            new Rss_Item 
                            { Title = item.Element(ns + "title").Value, 
                                Link = item.Element(ns + "link").Attribute("href").Value, 
                                Description = item.Element(ns + "content").Value,
                                PubDate = item.Element(ns + "published") != null ? Rfc822DateTime.Parse(item.Element(ns + "published").Value) : DateTime.MinValue            
                            }).Take(count);

                    // Invalid
                else
                    return null;*/
                return null;
            }
            finally
            {
                CacheResponse(Context, cacheDuration);
            }
        }

        private void CacheResponse(HttpContext context, int durationInMinutes)
        {
            TimeSpan duration = TimeSpan.FromMinutes(durationInMinutes);

            // With the new AJAX ASMX handler, there's no need for this hack to set maxAge value
            /*FieldInfo maxAge = HttpContext.Current.Response.Cache.GetType().GetField("_maxAge", BindingFlags.Instance | BindingFlags.NonPublic);
         maxAge.SetValue(HttpContext.Current.Response.Cache, duration);*/

            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetExpires(DateTime.Now.Add(duration));
            context.Response.Cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
            context.Response.Cache.SetMaxAge(duration);
        }
    }
}