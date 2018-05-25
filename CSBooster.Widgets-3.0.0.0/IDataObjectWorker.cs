using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IDataObjectWorker
    {
        DataObject DataObject { get; set; }
        string FolderParams { get; set; }
        QuickParameters QuickParameters { get; set; }
    }
}