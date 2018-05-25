using System.Web.UI;

namespace _4screen.CSB.Widget
{
    public class WidgetHelper
    {
        public static Control GetWidgetHost(Control currentControl, int currentRecursion, int maxRecursions)
        {
            if (currentControl.Parent is IWidgetHost)
            {
                return currentControl.Parent;
            }
            else
            {
                if (currentRecursion < maxRecursions && currentControl.Parent != null)
                {
                    return GetWidgetHost(currentControl.Parent, currentRecursion + 1, maxRecursions);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}