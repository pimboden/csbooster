// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
namespace _4screen.CSB.Widget
{
    //This interface is implemented By simple Controls that don't need to much info
    public interface IMinimalControl
    {
        string Prop1 { get; set; }
        string Prop2 { get; set; }
        bool HasContent { get; set; }
    }
}
