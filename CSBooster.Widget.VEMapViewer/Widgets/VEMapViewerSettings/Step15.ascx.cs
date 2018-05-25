using System;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;

namespace _4screen.CSB.Widget
{
    public partial class VEMapViewerSettings_Step15 : StepsASCX
    {
        private Guid InstanceID;
        private XmlDocument xmlDom = null;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InstanceID = ObjectID.Value;
            string strXml = LoadInstanceData(InstanceID);

            xmlDom = new XmlDocument();
            if (!string.IsNullOrEmpty(strXml))
            {
                xmlDom.LoadXml(strXml);
            }

            int anzahl = XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbAnz", 10000);
            try
            {
                rcbAnz.SelectedIndex = rcbAnz.Items.IndexOf(rcbAnz.Items.FindItemByValue(anzahl.ToString()));
            }
            catch
            {
                rcbAnz.SelectedIndex = 0;
            }
            txtLat.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtLat", "46.86770273172813");
            txtLong.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtLong", "8.3990478515625");
            txtZoom.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtZoom", "7");
            txtECSS.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtECSS", string.Empty);
            txtIP.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtIP", string.Empty);
            txtPFX.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtPFX", string.Empty);
            string bildTyp = XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbPT", "png");
            rcbPT.SelectedIndex = rcbPT.Items.IndexOf(rcbPT.Items.FindItemByValue(bildTyp));
            rcbMS.SelectedIndex = rcbMS.Items.IndexOf(rcbMS.Items.FindItemByValue(XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbMS", "3")));
            rcbMM.SelectedIndex = rcbMM.Items.IndexOf(rcbMM.Items.FindItemByValue(XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbMM", "1")));
            rcbNC.SelectedIndex = rcbNC.Items.IndexOf(rcbNC.Items.FindItemByValue(XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbNC", "1")));
            txtMH.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtMH", "400px");
            txtMW.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtMW", "400px");
            txtLPW.Text = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtLPW", "200px");
        }

        public override bool SaveStep(int NextStep)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                base.SaveStep(NextStep);
                try
                {
                    if (string.IsNullOrEmpty(txtLat.Text.Trim()))
                        txtLat.Text = "46.86770273172813";
                    else
                    {
                        double dblLat = 0.0;
                        if (!double.TryParse(txtLat.Text.Trim(), out dblLat))
                        {
                            txtLat.Text = "46.86770273172813";
                        }
                    }
                    if (string.IsNullOrEmpty(txtLong.Text.Trim()))
                        txtLong.Text = "8.3990478515625";
                    else
                    {
                        double dblLong = 0.0;
                        if (!double.TryParse(txtLong.Text.Trim(), out dblLong))
                        {
                            txtLong.Text = "8.3990478515625";
                        }
                    }
                    if (string.IsNullOrEmpty(txtZoom.Text.Trim()))
                        txtZoom.Text = "7";
                    else
                    {
                        int zoomLevel = 7;
                        if (int.TryParse(txtZoom.Text.Trim(), out zoomLevel))
                        {
                            if (zoomLevel > 19)
                                txtZoom.Text = "19";
                            else if (zoomLevel < 1)
                                txtZoom.Text = "1";
                        }
                        else
                            txtZoom.Text = "7";
                    }
                    Unit uMW = Unit.Pixel(400);
                    Unit uMH = Unit.Pixel(400);
                    Unit uLPW = Unit.Pixel(200);
                    try
                    {
                        uMW = Unit.Parse(txtMW.Text);
                    }
                    catch
                    {
                        try
                        {
                            uMW = Unit.Pixel(Convert.ToInt32(txtMW.Text));
                        }
                        catch
                        {
                        }
                    }
                    try
                    {
                        uMH = Unit.Parse(txtMH.Text);
                    }
                    catch
                    {
                        try
                        {
                            uMH = Unit.Pixel(Convert.ToInt32(txtMH.Text));
                        }
                        catch
                        {
                        }
                    }
                    try
                    {
                        uLPW = Unit.Parse(txtLPW.Text);
                    }
                    catch
                    {
                        try
                        {
                            uLPW = Unit.Pixel(Convert.ToInt32(txtLPW.Text));
                        }
                        catch
                        {
                        }
                    }
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtLat", txtLat.Text.Trim());
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtLong", txtLong.Text.Trim());
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtZoom", txtZoom.Text.Trim());
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "rcbAnz", rcbAnz.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtECSS", txtECSS.Text.Trim());
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtIP", txtIP.Text.Trim());
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtPFX", txtPFX.Text.Trim());
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "rcbPT", rcbPT.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "rcbMS", rcbMS.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "rcbMM", rcbMM.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "rcbNC", rcbNC.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtMH", uMH.ToString());
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtMW", uMW.ToString());
                    XmlHelper.SetElementInnerText(xmlDom.DocumentElement, "txtLPW", uLPW.ToString());
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

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}