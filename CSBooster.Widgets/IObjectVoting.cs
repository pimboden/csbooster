using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IObjectVoting
    {
        DataObject DataObject { get; set; }

        bool ShowInfo { get; set; }
    }
}