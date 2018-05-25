//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System.Windows.Forms;

namespace _4screen.CSB.Monitor
{
  public partial class ServiceForm : DevExpress.XtraEditors.XtraForm
  {
    public ServiceForm()
    {
      InitializeComponent();
      this.Text = "Server Monitor";
    }

    public void AppendMessage(string message)
    {
      this.memoEdit1.Text = message + "\r\n" + this.memoEdit1.Text;
    }
  }
}
