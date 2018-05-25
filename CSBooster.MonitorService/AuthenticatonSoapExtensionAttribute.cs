//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services.Protocols;

namespace _4screen.CSB.MonitorService
{
  [AttributeUsage(AttributeTargets.Method)]
  public class AuthenticatonSoapExtensionAttribute : SoapExtensionAttribute
  {
    private int _priority;
    public override int Priority
    {
      get { return _priority; }
      set { _priority = value; }
    }

    public override Type ExtensionType
    {
      get { return typeof(AuthenticatonSoapExtension); }
    }
  }
}
