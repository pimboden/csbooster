//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.20  1.0.0.4  AW  Initial release
//*****************************************************************************************

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace _4screen.CSB.Monitor
{
  [Serializable]
  [System.Xml.Serialization.XmlInclude(typeof(MonitorControlUsersProperties))]
  public class MonitorControlFeaturedContentEditProperties : MonitorControlProperties
  {
     private MonitorControlFeaturedContentEdit control;
    private string serviceLocation;

     public MonitorControlFeaturedContentEditProperties()
      : base()
    {
       this.control = new MonitorControlFeaturedContentEdit(this);
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
