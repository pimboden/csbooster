//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		03.10.2007 / TS
//                         handle the help business database functions
//  Updated:   
//******************************************************************************
namespace _4screen.CSB.DataAccess.Business
{
    public static class Help
    {
        /// <summary>
        /// // load the short help text for the given ID's
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="subID"></param>
        /// <param name="langCd"></param>
        /// <returns></returns>
        public static string GetShortHelp(string ID, string subID, string langCd)
        {
            if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(subID) && !string.IsNullOrEmpty(langCd))
                return Data.Help.GetShortHelp(ID, subID, langCd);
            else
                return string.Empty;
        }

        /// <summary>
        /// // load the long help text for the given ID's
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="subID"></param>
        /// <param name="langCd"></param>
        /// <returns></returns>
        public static string GetLongHelp(string ID, string subID, string langCd)
        {
            if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(subID) && !string.IsNullOrEmpty(langCd))
                return Data.Help.GetLongHelp(ID, subID, langCd);
            else
                return string.Empty;
        }
    } // END CLASS
} // END NAMESPACE