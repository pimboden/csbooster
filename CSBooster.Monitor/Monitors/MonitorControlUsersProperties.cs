//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace _4screen.CSB.Monitor
{
  [Serializable]
  [System.Xml.Serialization.XmlInclude(typeof(MonitorControlUsersProperties))]
  public class MonitorControlUsersProperties : MonitorControlProperties
  {
    private MonitorControlUsers control;
    private string serviceLocation;

    public MonitorControlUsersProperties()
      : base()
    {
      this.control = new MonitorControlUsers(this);
    }

    [Category("Service")]
    [DisplayName("Service Standort")]
    [TypeConverter(typeof(ServiceLocationConverter))]
    public string ServiceLocation
    {
      get { return this.serviceLocation; }
      set
      {
        this.serviceLocation = value;
        this.control.Reload();
      }
    }

    public override Control GetControl()
    {
      return control;
    }
  }
}
