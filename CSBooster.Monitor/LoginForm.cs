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
   public partial class LoginForm : DevExpress.XtraEditors.XtraForm
   {
      private CSBoosterMonitor csboosterMonitor;

      public LoginForm(CSBoosterMonitor csboosterMonitor)
      {
         InitializeComponent();

         this.csboosterMonitor = csboosterMonitor;
      }

      private void OnLoginKeyPress(object sender, KeyPressEventArgs e)
      {
         if (e.KeyChar == (char)Keys.Enter)
         {
            OnLoginButtonClick(null, null);
         }
      }

      private void OnLoginButtonClick(object sender, EventArgs e)
      {
         this.Hide();
         /*this.usernameTextBox.Text = "hansruedi";
         this.passwordTextBox.Text = "asdf";*/

         if (this.csboosterMonitor.Login(this.usernameTextBox.Text, this.passwordTextBox.Text) == true)
         {
            this.Close();
         }
         else
         {
            this.Show();
            MessageBox.Show("Anmeldung fehlgeschlagen! Bitte versuchen sie es noch einmal", "Anmeldung fehlgeschlagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }
   }
}
