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

namespace _4screen.CSB.MonitorService
{
  public class ChartDataPair : IComparable<ChartDataPair>
  {
    private string id;
    private int value;

    public ChartDataPair()
    {
    }

    public ChartDataPair(string id, int value)
    {
      this.id = id;
      this.value = value;
    }

    public string Id
    {
      get { return this.id; }
      set { this.id = value; }
    }

    public int Value
    {
      get { return this.value; }
      set { this.value = value; }
    }

    public int CompareTo(ChartDataPair x)
    {
      return x.Value.CompareTo(this.Value);
    }
  }
}
