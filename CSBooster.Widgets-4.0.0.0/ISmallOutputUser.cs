// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface ISmallOutputUser
    {
        DataObjectUser DataObjectUser { get; set; }
        string UserName { get; set; }
        string UserPictureURL { get; set; }
        string PrimaryColor { get; set; }
        string SecondaryColor { get; set; }
        string UserDetailURL { get; set; }
    }
}
