//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System.Runtime.Serialization;
using System.Windows.Forms;

namespace _4screen.CSB.Monitor
{
  public abstract class MonitorControlProperties
  {
    public MonitorControlProperties()
    {
      CSBoosterMonitor.AddSerializableType(this.GetType());
    }

    public virtual Control GetControl()
    {
      return null;
    }
  }
}
