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
using System.Diagnostics;

namespace _4screen.CSB.MonitorService
{
  // Custom SoapExtension that authenticates the method being called
  public class AuthenticatonSoapExtension : SoapExtension
  {
    // When overridden in a derived class, allows a SOAP extension to initialize data specific to an XML Web service method using an attribute applied to the XML Web service method at a one time performance cost
    public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attrib)
    {
      return null;
    }

    // When overridden in a derived class, allows a SOAP extension to initialize data specific to a class implementing an XML Web service at a one time performance cost
    public override object GetInitializer(Type WebServiceType)
    {
      return null;
    }

    // When overridden in a derived class, allows a SOAP extension to initialize itself using the data cached in the GetInitializer method
    public override void Initialize(object initializer)
    {

    }

    // After the message is deserialized we authenticate it
    //[DebuggerNonUserCode()]
    public override void ProcessMessage(SoapMessage message)
    {
      if (message.Stage == SoapMessageStage.AfterDeserialize)
      {
        Authenticate(message);
      }
    }

    //[DebuggerNonUserCode()]
    public void Authenticate(SoapMessage message)
    {
      AuthenticationHeader header = message.Headers[0] as AuthenticationHeader;
      if (header != null)
      {
        if (!Authorization.IsWebMethodPermitted(header.Username, header.Password, message.MethodInfo.Name))
        {
          throw new SoapException("Fehlende Berechtigung für '" + message.MethodInfo.Name+"'", SoapException.ServerFaultCode);
        }
      }
      else
      {
        throw new SoapException("Authentifizierung fehlgeschlagen", SoapException.ServerFaultCode);
      }
    }
  }
}
