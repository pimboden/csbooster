// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebUI
{
    public interface IViewHandler
    {
        DataObject DataObject { get; set; }
        DataObject ParentDataObject { get; set; }

        ObjectStatus GetObjectStatus();
        bool IsManaged();
        string GetRoles();
        FriendType GetFriendType();
    }
}
