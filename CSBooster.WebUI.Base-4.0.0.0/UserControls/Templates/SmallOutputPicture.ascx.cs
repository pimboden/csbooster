// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class SmallOutputPicture : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        public DataObject DataObject { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Img.ImageUrl = SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            Img.ToolTip = DataObject.Title;
            LnkTitle.Text = DataObject.Title.CropString(18);
            LnkTitle.ToolTip = DataObject.Title;
            LnkTitle.NavigateUrl = Helper.GetDetailLink(DataObject.ObjectType, DataObject.ObjectID.Value.ToString());
            LnkImg.Attributes.Add("onmousedown", "setQMCookie('qm" + Helper.GetObjectType(DataObject.ObjectType).Id + "', '" + Settings["WidgetInstanceId"] + "')");
            LnkImg.NavigateUrl = LnkTitle.NavigateUrl;
            LnkOwner.Text = DataObject.Nickname.CropString(12);
            LnkOwner.ToolTip = DataObject.Nickname;
            LnkOwner.NavigateUrl = Helper.GetDetailLink("User", DataObject.Nickname);
            LnkOwner.ID = null;
            Img.ID = null;
            LnkTitle.ID = null;
            LnkImg.ID = null;
        }
    }
}