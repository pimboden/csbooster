//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	21.0.0.0		23.07.2008 AW
//  Updated:   
//******************************************************************************

using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.Extensions.Business
{
    public class SessionLogParams
    {
        public SessionLogParams(UserDataContext udc)
        {
            this.SessionID = udc.UserSessionID.CropString(88);
            if (udc.IsAuthenticated) 
                this.UserID = udc.UserID;

            this.IP = udc.UserIP.CropString(20);
            this.IsMobileDevice = udc.IsMobileDevice;
            this.IsIPhone = udc.IsIPhone;
            this.BrowserType = udc.BrowserType.CropString(50);
            this.BrowserVersion = udc.BrowserVersion.CropString(20);
            this.System = udc.SystemType.CropString(30);
            this.SystemVersion = udc.SystemVersion.CropString(60);
            this.UserLanguage = udc.UserLanguages.CropString(200);
            this.Roles = udc.UserRole.CropString(200);
            this.BrowserID = udc.BrowserID; 
         }

        public string SessionID { get; private set; }
        public Guid? UserID { get; private set; }
        public string IP { get; private set; }
        public bool IsMobileDevice { get; private set; }
        public bool IsIPhone { get; private set; }
        public string BrowserType { get; private set; }
        public string BrowserVersion { get; private set; }
        public string System { get; private set; }
        public string SystemVersion { get; private set; }
        public string UserLanguage { get; private set; }
        public string Roles { get; private set; }
        public Guid? BrowserID { get; private set; }
    }
}