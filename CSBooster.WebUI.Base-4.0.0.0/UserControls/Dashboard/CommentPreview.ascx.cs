//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		06.09.2007 / AW
//  Updated:   
//******************************************************************************

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class CommentPreview : System.Web.UI.UserControl
    {
        private DataObjectComment comment;
        private string type;

        public DataObjectComment Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public bool StripHtml { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(500);
            if (type.Equals("Comment"))
            {
                sb.AppendFormat("<div class=\"commentInfo\">{0}</div>", comment.Inserted.ToShortDateString() + " " + comment.Inserted.ToShortTimeString());
                if (StripHtml)
                    sb.AppendFormat("<div>{0}</div>", comment.Description.StripHTMLTags().CropString(300));
                else
                    sb.AppendFormat("<div>{0}</div>", comment.DescriptionLinked);
                Ph.Controls.Add(new LiteralControl(sb.ToString()));
            }
            else if (type.Equals("Object"))
            {
                HyperLink link = new HyperLink();
                QuickParameters quickParams = new QuickParameters
                                                  {
                                                      Udc = UserDataContext.GetUserDataContext(),
                                                      QuerySourceType = QuerySourceType.Profile,
                                                      RelationParams = new RelationParams
                                                                           {
                                                                               ChildObjectID = comment.ObjectID
                                                                           }
                                                  };
                DataObjectList<DataObject> dataObjects = DataObjects.Load<DataObject>(quickParams);
                if (dataObjects.Count == 1)
                {
                    DataObject dataObject = dataObjects[0];
                    link.NavigateUrl = Helper.GetDetailLink(dataObject.ObjectType, dataObject.ObjectID.ToString());
                    sb.AppendFormat("<div>{0}</div>", Helper.GetObjectName(dataObject.ObjectType, true));
                    sb.AppendFormat("<img src=\"{0}{1}\">", SiteConfig.MediaDomainName, dataObject.GetImage(PictureVersion.XS));
                }
                else
                {
                    sb.AppendFormat("---");
                }
                link.Text = sb.ToString();
                Ph.Controls.Add(link);
            }
        }
    }
}