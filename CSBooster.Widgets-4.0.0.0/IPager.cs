// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
namespace _4screen.CSB.Widget
{
    public interface IPager
    {
        System.Web.UI.Control BrowsableControl { get; set; }
       
        string ButtonFirstUrlActive { get; set; }
       
        string ButtonFirstUrlInactive { get; set; }
       
        string ButtonLastUrlActive { get; set; }
        
        string ButtonLastUrlInactive { get; set; }
       
        string ButtonNextUrlActive { get; set; }
        
        string ButtonNextUrlInactive { get; set; }
        
        string ButtonPrevUrlActive { get; set; }
        
        string ButtonPrevUrlInactive { get; set; }
        
        string CssClassButtons { get; set; }
        
        string CssClassGoToActive { get; set; }
        
        string CssClassGoToInactive { get; set; }
        
        string CssClassPager { get; set; }
        
        string CustomText { get; set; }
        
        string ItemNamePlural { get; set; }
        
        string ItemNameSingular { get; set; }

        bool RenderHref { get; set; }
        
        int PagerBreak { get; set; }
        
        int PageSize { get; set; }

        int CheckPageRange(int currentPage, int numberItems); 
        
        void InitPager(int currentPage, int numberItems);
    }
}
