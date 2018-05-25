//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************

using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget.UserControls.Templates
{
    public partial class UserLists : System.Web.UI.UserControl, IDataObjectWorker
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetUserLists");
        protected string MediaDomainName = SiteConfig.MediaDomainName;
        protected string SiteVRoot = SiteConfig.SiteVRoot;

        public DataObject DataObject { get; set; }

        public string FolderParams { get; set; }
        public QuickParameters QuickParameters { get; set; }

        protected DataObjectUser DataObjectUser
        {
            get { return (DataObjectUser)DataObject; }
        }

        protected string UserPictureURL(PictureVersion size)
        {
            return MediaDomainName + DataObjectUser.GetImage(size);
        }

        protected string UserDetailURL
        {
            get
            {
                return SiteVRoot + Helper.GetDetailLink(2, DataObjectUser.Nickname);
            }
        }

        protected string UserPrimaryColorLargeURL
        {
            get
            {
                return string.Format("{0}{1}{2}.png", SiteVRoot, "/Library/Images/User_Icon/lg/pc/", DataObjectUser.PrimaryColor);
            }
        }

        protected string UserSecondaryColorLargeURL
        {
            get
            {
                return string.Format("{0}{1}{2}.png", SiteVRoot, "/Library/Images/User_Icon/lg/sc/", DataObjectUser.SecondaryColor);
            }
        }

        protected string UserPrimaryColorSmallURL
        {
            get
            {
                return string.Format("{0}{1}{2}.png", SiteVRoot, "/Library/Images/User_Icon/sm/pc/", DataObjectUser.PrimaryColor);
            }
        }

        protected string UserSecondaryColorSmallURL
        {
            get
            {
                return string.Format("{0}{1}{2}.png", SiteVRoot, "/Library/Images/User_Icon/sm/sc/", DataObjectUser.SecondaryColor);
            }
        }

    }
}