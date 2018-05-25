// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************

namespace _4screen.CSB.WidgetControls.Common
{
    public static class Helper
    {
        public static void Ddl_SelectItem(Telerik.Web.UI.RadComboBox Ddl, int Value)
        {
            Ddl_SelectItem(Ddl, Value.ToString());
        }

        public static void Ddl_SelectItem(Telerik.Web.UI.RadComboBox Ddl, string Value)
        {
            if (Ddl.Items.FindItemByValue(Value) != null)
                Ddl.SelectedIndex = Ddl.Items.IndexOf(Ddl.Items.FindItemByValue(Value));
            else if (Ddl.Items.Count > 0)
                Ddl.SelectedIndex = 0;
        }
    }
}