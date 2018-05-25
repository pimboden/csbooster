using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class Image : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            DataObjectCommunity community = new DataObjectCommunity(UserDataContext.GetUserDataContext());
            community.ObjectID = CommunityID.ToString();
            community.Load();
            if (community.ObjectType == ObjectType.ProfileCommunity)
            {
                DataObjectUser user = new DataObjectUser(UserDataContext.GetUserDataContext());
                user.ObjectID = community.UserID;
                user.Load();

                string image = user.GetImage(PictureVersion.S);
                if (string.IsNullOrEmpty(image))
                    IMAGE.ImageUrl = SiteContext.MediaDomainName + Constants.DEFIMG_USER;
                else
                    IMAGE.ImageUrl = SiteContext.MediaDomainName + image;
            }
            else
            {
                string image = community.GetImage(PictureVersion.S);
                if (string.IsNullOrEmpty(image))
                    IMAGE.ImageUrl = SiteContext.MediaDomainName + Constants.DEFIMG_COMMUNITY;
                else
                    IMAGE.ImageUrl = SiteContext.MediaDomainName + image;
            }
            return true;
        }
    }
}