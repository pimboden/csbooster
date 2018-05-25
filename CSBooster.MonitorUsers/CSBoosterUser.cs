using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Web.Security;

namespace CSBooster.MonitorUsers
{
	public partial class CSBoosterUser : Form
	{
		private MonitorDBDataContext mdbDC;
		public CSBoosterUser()
		{
			InitializeComponent();
			lblMsg.Text = string.Empty;
			mdbDC = new MonitorDBDataContext();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			MembershipUser memUser = Membership.GetUser(txtUserName.Text, false);
			if (memUser != null)
			{
				string newPwd = txtPWD.Text.Trim();
				if (newPwd.Length > 0)
				{
					string oldPWD = memUser.ResetPassword();
					Membership.UpdateUser(memUser);
					memUser.ChangePassword(oldPWD, newPwd);
					Membership.UpdateUser(memUser);
				}
				Guid UserId = new Guid( memUser.ProviderUserKey.ToString());
				monitor_WebMethodsProhibited webMethPrhib = (from webMethodsPrhibited in mdbDC.monitor_WebMethodsProhibiteds.Where(x => x.UserId == UserId)
										 select (webMethodsPrhibited)).FirstOrDefault();
				monitor_Role role1 = (from moniRoles in mdbDC.monitor_Roles.Where(s=>s.UserId == UserId && s.Role == "StatisticsViewer")
								select (moniRoles)).FirstOrDefault();
				monitor_Role role2 = (from moniRoles in mdbDC.monitor_Roles.Where(s => s.UserId == UserId && s.Role == "UserManager")
											 select (moniRoles)).FirstOrDefault();
				if (!cbxRole1.Checked &&!cbxRole2.Checked)
				{
					if (webMethPrhib != null) mdbDC.monitor_WebMethodsProhibiteds.DeleteOnSubmit(webMethPrhib);
					if (role1 != null) mdbDC.monitor_Roles.DeleteOnSubmit(role1);
					if (role2 != null) mdbDC.monitor_Roles.DeleteOnSubmit(role2);
				}
				else if (cbxRole1.Checked || cbxRole2.Checked)
				{
					if (webMethPrhib == null)
					{
						webMethPrhib = new monitor_WebMethodsProhibited() { UserId = UserId, WebMethod = "Dummy" };
						mdbDC.monitor_WebMethodsProhibiteds.InsertOnSubmit(webMethPrhib);
					}
					if (cbxRole1.Checked && role1 == null)
					{
						role1 = new monitor_Role() { IsMember = true, Role = "StatisticsViewer", UserId = UserId };
						mdbDC.monitor_Roles.InsertOnSubmit(role1);
					}
					else if (!cbxRole1.Checked && role1 != null)
					{
						mdbDC.monitor_Roles.DeleteOnSubmit(role1);
					}
					if (cbxRole2.Checked && role2 == null)
					{
						role2 = new monitor_Role() { IsMember = true, Role = "UserManager", UserId = UserId };
						mdbDC.monitor_Roles.InsertOnSubmit(role2);
					}
					else if (!cbxRole2.Checked && role2 != null)
					{
						mdbDC.monitor_Roles.DeleteOnSubmit(role2);
					}
				}
				mdbDC.SubmitChanges();
			}
			else
			{
				lblMsg.Text = "Benutzer wurde nicht gefunden";
			}
		}

	}
}
