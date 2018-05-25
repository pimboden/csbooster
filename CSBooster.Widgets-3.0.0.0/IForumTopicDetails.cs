using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IForumTopicDetails
    {
        DataObject DataObject { get; set; }

        int PageSize { get; set; }

        int PagerBreak { get; set; }
    }
}