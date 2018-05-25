// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
namespace _4screen.CSB.DataAccess.Data
{
    /// <summary>
    /// BadWordFilterActions (several actions per word -> ';' separated)
    /// This enumeration needs binary numbering for logical conjunction
    /// </summary>
    internal enum BadWordFilterActions
    {
        None = 0,
        Censor = 1,
        Inform = 2,
        Lock = 4
    }

    /// <summary>
    /// AdWordFilterActions (one action per word)
    /// </summary>
    internal enum AdWordFilterActions
    {
        Link,
        Popup
    }

    /// <summary>
    /// Used by manually called filter methods
    /// </summary>
    public enum FilterObjectTypes
    {
        DataObject,
        Profile,
        Comment
    }
}