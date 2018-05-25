// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebUI.MasterPages
{
    public partial class MobileMaster : System.Web.UI.MasterPage
	{
		private UserDataContext udc;
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			udc = UserDataContext.GetUserDataContext();
            /*string strAcceptHeader = Request.Headers["accept"];
			if (string.IsNullOrEmpty(strAcceptHeader))
			{
				Response.ContentType = "text/html";
			}
			else if (strAcceptHeader.IndexOf("application/vnd.wap.xhtml+xml") != -1)
			{
				Response.ContentType = "application/vnd.wap.xhtml+xml";
			}
			else if (strAcceptHeader.IndexOf("application/xhtml+xml") != -1)
			{
				Response.ContentType = "application/xhtml+xml";
            }*/
			if (udc.IsIPhone)
                litHeaders.Text += "<meta name=\"viewport\" content=\"width=320\" />";
		}


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			XDocument doc = Helper.LoadMobileConfig();
			Partner partner = null;
			if (!string.IsNullOrEmpty(Request.QueryString["ParID"]))
			{
				partner = Partner.Get(null, UserProfile.Current.UserId);
			}
			if (PartnerHeader != null)
			{
				string Header = string.Empty;
				var config = doc.Element("configuration");
				Header = (from settings in config.Elements("setting").Where(y => (y.Attribute("Key").Value == "MP_DefaultHeader"))
							 select settings.FirstNode.ToString()
													).FirstOrDefault();


				if (partner != null)
				{
					Header = partner.MobileHeader;
				}
					Header = GetWraper("*", Header);
				PartnerHeader.Text = Header;
			}
			if (PartnerFooter != null)
			{
				string Footer = string.Empty;
				var config = doc.Element("configuration");
				Footer = (from settings in config.Elements("setting").Where(y => (y.Attribute("Key").Value == "MP_DefaultFooter"))
							 select settings.FirstNode.ToString()
													).FirstOrDefault();


				if (partner != null)
				{
					Footer = partner.MobileFooter;
				}
					Footer = GetWraper("*", Footer);
				PartnerFooter.Text = Footer;
			}
			Page.Header.DataBind();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		private string GetWraper(string DeviceAtt, string Wrapper)
		{
			XDocument doc = XDocument.Parse(Wrapper);
			var items = doc.Element("items");
			string retWraper = (from wrap in items.Elements("item").Where(y=> y.Attribute("Device").Value == DeviceAtt)
									 select wrap.Value).FirstOrDefault();
		  return retWraper;
		}
	}
}