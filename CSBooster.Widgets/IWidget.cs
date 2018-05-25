//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************
namespace _4screen.CSB.Widget
{
    public interface IWidget
    {
        void Init(IWidgetHost host);
        void ShowSettings();
        void HideSettings();
        void Minimized();
        void Maximized();
        void Closed();
    }
}