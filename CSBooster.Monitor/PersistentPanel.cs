//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;

namespace _4screen.CSB.Monitor
{
  public class PersistentPanel
  {
    private Guid panelId;
    private MonitorControlProperties monitorControlProperties;

    public PersistentPanel()
    {
    }

    public PersistentPanel(Guid panelId, MonitorControlProperties monitorControlProperties)
    {
      this.panelId = panelId;
      this.monitorControlProperties = monitorControlProperties;
    }

    public Guid PanelId
    {
      get { return this.panelId; }
      set { this.panelId = value; }
    }

    public MonitorControlProperties MonitorControlProperties
    {
      get { return this.monitorControlProperties; }
      set { this.monitorControlProperties = value;  }
    }
  }
}
