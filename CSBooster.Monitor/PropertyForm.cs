//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.Windows.Forms;

namespace _4screen.CSB.Monitor
{
  public partial class PropertyForm : DevExpress.XtraEditors.XtraForm
  {
    private static PropertyForm instance;

    private PropertyForm()
    {
      InitializeComponent();
    }

    public static PropertyForm GetInstance()
    {
      // If the form has never been created or has been closed before
      if (instance == null || instance.IsDisposed)
      {
        instance = new PropertyForm();
      }
      instance.TopMost = true;
      return instance;
    }

    public void SetSelectedObject(object selectedObject)
    {
      this.propertyGridControl1.SelectedObject = selectedObject;

      // Center align numeric fields
      foreach (DevExpress.XtraVerticalGrid.Rows.BaseRow baseRow in this.propertyGridControl1.VisibleRows)
      {
        if(baseRow is DevExpress.XtraVerticalGrid.Rows.EditorRow && baseRow.Properties.RowType == typeof(Int32))
          baseRow.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
      }
    }
  }
}
