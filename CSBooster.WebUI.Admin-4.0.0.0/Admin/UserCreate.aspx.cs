// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class UserCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnCreateClick(object sender, EventArgs e)
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Text;
            string email = TxtEmail.Text;
            string firstname = TxtFirstname.Text;
            string lastname = TxtLastname.Text;

            Regex usernameRegex = new Regex(Constants.REGEX_USERNAME);
            Regex emailRegex = new Regex(Constants.REGEX_EMAIL);

            if (string.IsNullOrEmpty(username))
            {
                PnlStatus.Visible = true;
                LitStatus.Text = "Bitte geben Sie einen Benutzernamen ein";
            }
            else if (!usernameRegex.IsMatch(username))
            {
                PnlStatus.Visible = true;
                LitStatus.Text = "Bitte geben Sie einen gültigen Benutzernamen ein";
            }
            else if (Membership.GetUser(username) != null)
            {
                PnlStatus.Visible = true;
                LitStatus.Text = "Dieser Benutzername ist bereits besetzt. Bitte geben Sie einen anderen Benutzernamen ein";
            }
            else if (string.IsNullOrEmpty(password))
            {
                PnlStatus.Visible = true;
                LitStatus.Text = "Bitte geben Sie ein Passwort ein";
            }
            else if (string.IsNullOrEmpty(email))
            {
                PnlStatus.Visible = true;
                LitStatus.Text = "Bitte geben Sie eine Email-Adresse ein";
            }
            else if (!emailRegex.IsMatch(email))
            {
                PnlStatus.Visible = true;
                LitStatus.Text = "Bitte geben Sie einen gültige Email-Adresse ein";
            }
            else if (Membership.GetUserNameByEmail(email) != null)
            {
                PnlStatus.Visible = true;
                LitStatus.Text = "Diese Email-Adresse ist bereits besetzt. Bitte geben Sie eine andere  Email-Adresse ein";
            }
            else if (DataObjectUser.CreateUser(AuthenticationType.CSBooster, null, username, email, firstname, lastname, null))
            {
                MembershipUser membershipUser = Membership.GetUser(username);
                string generatedPassword = membershipUser.ResetPassword();
                membershipUser.ChangePassword(generatedPassword, password);

                if (((LinkButton)sender).CommandArgument == "CreateAndLogin")
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    Response.Redirect("/");
                }
                else
                {
                    TxtUsername.Text = string.Empty;
                    TxtEmail.Text = string.Empty;
                    TxtFirstname.Text = string.Empty;
                    TxtLastname.Text = string.Empty;
                }
            }
            else
            {
                PnlStatus.Visible = true;
                LitStatus.Text = "Benutzer konnte nicht erstellt werden";
            }
        }
    }
}