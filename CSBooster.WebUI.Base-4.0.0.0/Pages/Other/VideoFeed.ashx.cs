using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4screen.CSB.WebUI.Pages.Other
{
    public class VideoFeed : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string objectID = HttpContext.Current.Request.QueryString["OID"];

            context.Response.ContentType = "application/rss+xml";
            context.Response.Write(_4screen.CSB.DataAccess.Business.DataObjectVideo.GetPlaylistFeed(new Guid(objectID)));
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
