//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		17.08.2007 / PI
//                         Inherits StepsASCX
//                         Step with Basic Info
//  Updated:   
//******************************************************************************

using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
	public partial class TestWidgetSettings_Step10 : StepsASCX
	{
		#region FIELDS

		private Guid InstanceID;

		#endregion FIELDS

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			InstanceID = new Guid(this.ObjectID);
			string strXml = LoadInstanceData(InstanceID);

			XmlDocument xmlDom = new XmlDocument();
			if (!string.IsNullOrEmpty(strXml))
			{
				xmlDom.LoadXml(strXml);
			}

			cbxUsr.Checked = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxUsr", true);
			cbxTit.Checked = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxTit", false);
			cbxDesc.Checked = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxDesc", false);
			cbxRating.Checked = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxRating", true);
			txtImg.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtImg", "");
			ddlImgWidth.SelectedValue = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ddlImgWidth", "300");

			LoadImages();
		}

		#region PRIVATE METHODES

		public override bool SaveStep(int NextStep)
		{
			Page.Validate();
			if (Page.IsValid)
			{
				base.SaveStep(NextStep);
				try
				{
					XmlDocument xmlDom = new XmlDocument();
					XmlHelper.CreateRoot(xmlDom, "root");

					xmlDom.DocumentElement.SetAttribute("version", "2.0");

					XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "cbxUsr", cbxUsr.Checked);
					XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "cbxTit", cbxTit.Checked);
					XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "cbxDesc", cbxDesc.Checked);
					XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "cbxRating", cbxRating.Checked);
					XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "ddlImgWidth", ddlImgWidth.SelectedValue);

					if (txtImg.Text == "CSBNo")
						XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtImg", "");
					else
						XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtImg", txtImg.Text);

					return SaveInstanceData(InstanceID, xmlDom.OuterXml);
				}
				catch
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private void LoadImages()
		{
			bool blnHasMoreRecords;
			StringBuilder sb = new StringBuilder();

			List<QuickObjectPicture> listPictures = QuickObjectPictures.Load(SiteContext.Udc, communityID, string.Empty, string.Empty, QuickSort.InsertedDate, QuickSortDirection.Desc, QuickDataRange.All, 0, ObjectShowState.Published, out blnHasMoreRecords, null, null, true, null, true, null, null, null, null, null, null, null, null, null, null, false);
			if (listPictures.Count > 0)
			{
				if (txtImg.Text.Length == 0)
					sb.AppendFormat("<div class=\"CSB_wi_selected\" id=\"{0}\">", "CSBNo");
				else
					sb.AppendFormat("<div class=\"CSB_wi_not_selected\" id=\"{0}\">", "CSBNo");

				sb.AppendFormat("<img class=\"CSB_wi_preview_obj\" src=\"{1}/Library/Images/icomicro/ShowNoImg.png\" alt=\"{2}\" onclick=\"javascript:setimage('{0}');\" />", "CSBNo", SiteContext.VRoot, "irgendein Bild der Community nach dem Zusfallsprinzip anzeigen");
				sb.Append("</div>");

				foreach (QuickObjectPicture Item in listPictures)
				{
					if (txtImg.Text == Item.ID)
						sb.AppendFormat("<div class=\"CSB_wi_selected\" id=\"{0}\">", Item.ID);
					else
						sb.AppendFormat("<div class=\"CSB_wi_not_selected\" id=\"{0}\">", Item.ID);

					sb.AppendFormat("<img class=\"CSB_wi_preview_obj\" src=\"{1}{2}\" alt=\"{3}\" onclick=\"javascript:setimage('{0}');\" />", Item.ID, SiteContext.MediaDomainName, Item.URLImageSmall, Item.Title);

					if (Item.Title.Length > 10)
						sb.AppendFormat("<div>{0}</div>", Item.Title.Substring(0, 10) + "..");
					else
						sb.AppendFormat("<div>{0}</div>", Item.Title);

					sb.Append("</div>");
				}

				LitImg.Text = sb.ToString();
			}
		}

		#endregion PRIVATE METHODES

	}  // END CLASS
}  // END NAMESPACE