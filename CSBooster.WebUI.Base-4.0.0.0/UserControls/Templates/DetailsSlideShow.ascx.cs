// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsSlideShow : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        protected DataObject dataObject;
        private DataObjectSlideShow objSlideShow;
        private UserDataContext udc;

        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            objSlideShow = (DataObjectSlideShow)dataObject;
            udc = UserDataContext.GetUserDataContext();

            if (!IsPostBack)
            {
                LoadSlideShow();
                //LoadSilverlightSlideShow();
            }
        }

        private void LoadSlideShow()
        {
            XmlDocument xmlSlConfig = new XmlDocument();
            xmlSlConfig.Load(Server.MapPath("/configurations/SlideShow.config"));
            string CtyId = objSlideShow.CommunityID.Value.ToString();
            switch (objSlideShow.Effect)
            {
                case "None":
                    Show1.TransitionType = OboutInc.Show.TransitionType.None;
                    break;
                case "FadeFast":
                    Show1.TransitionType = OboutInc.Show.TransitionType.Fading;
                    try
                    {
                        XmlElement xmlEffect = (XmlElement)xmlSlConfig.SelectSingleNode("configuration/Effects/FadeFast");
                        Show1.FadingSpeed = Convert.ToInt32(xmlEffect.GetAttribute("FadingSpeed"));
                        Show1.FadingStep = Convert.ToInt32(xmlEffect.GetAttribute("FadingStep"));
                    }
                    catch
                    {
                        Show1.FadingSpeed = 5000;
                        Show1.FadingStep = 5;
                    }
                    break;
                case "FadeMedium":
                    Show1.TransitionType = OboutInc.Show.TransitionType.Fading;
                    try
                    {
                        XmlElement xmlEffect = (XmlElement)xmlSlConfig.SelectSingleNode("configuration/Effects/FadeMedium");
                        Show1.FadingSpeed = Convert.ToInt32(xmlEffect.GetAttribute("FadingSpeed"));
                        Show1.FadingStep = Convert.ToInt32(xmlEffect.GetAttribute("FadingStep"));
                    }
                    catch
                    {
                        Show1.FadingSpeed = 1000;
                        Show1.FadingStep = 3;
                    }
                    break;
                case "FadeSlow":
                    Show1.TransitionType = OboutInc.Show.TransitionType.Fading;
                    try
                    {
                        XmlElement xmlEffect = (XmlElement)xmlSlConfig.SelectSingleNode("configuration/Effects/FadeSlow");
                        Show1.FadingSpeed = Convert.ToInt32(xmlEffect.GetAttribute("FadingSpeed"));
                        Show1.FadingStep = Convert.ToInt32(xmlEffect.GetAttribute("FadingStep"));
                    }
                    catch
                    {
                        Show1.FadingSpeed = 100;
                        Show1.FadingStep = 1;
                    }
                    break;
                case "ScrollRight":
                    Show1.TransitionType = OboutInc.Show.TransitionType.Scrolling;
                    Show1.ScrollDirection = OboutInc.Show.ScrollDirection.Right;
                    try
                    {
                        XmlElement xmlEffect = (XmlElement)xmlSlConfig.SelectSingleNode("configuration/Effects/ScrollRight");
                        Show1.ScrollingSpeed = Convert.ToInt32(xmlEffect.GetAttribute("ScrollingSpeed"));
                        Show1.ScrollingStep = Convert.ToInt32(xmlEffect.GetAttribute("ScrollingStep"));
                    }
                    catch
                    {
                        Show1.ScrollingSpeed = 1000;
                        Show1.ScrollingStep = 3;
                    }
                    break;
                case "ScrollLeft":
                    Show1.TransitionType = OboutInc.Show.TransitionType.Scrolling;
                    Show1.ScrollDirection = OboutInc.Show.ScrollDirection.Left;
                    try
                    {
                        XmlElement xmlEffect = (XmlElement)xmlSlConfig.SelectSingleNode("configuration/Effects/ScrollLeft");
                        Show1.ScrollingSpeed = Convert.ToInt32(xmlEffect.GetAttribute("ScrollingSpeed"));
                        Show1.ScrollingStep = Convert.ToInt32(xmlEffect.GetAttribute("ScrollingStep"));
                    }
                    catch
                    {
                        Show1.ScrollingSpeed = 1000;
                        Show1.ScrollingStep = 3;
                    }
                    break;
                case "ScrollTop":
                    Show1.TransitionType = OboutInc.Show.TransitionType.Scrolling;
                    Show1.ScrollDirection = OboutInc.Show.ScrollDirection.Top;
                    try
                    {
                        XmlElement xmlEffect = (XmlElement)xmlSlConfig.SelectSingleNode("configuration/Effects/ScrollTop");
                        Show1.ScrollingSpeed = Convert.ToInt32(xmlEffect.GetAttribute("ScrollingSpeed"));
                        Show1.ScrollingStep = Convert.ToInt32(xmlEffect.GetAttribute("ScrollingStep"));
                    }
                    catch
                    {
                        Show1.ScrollingSpeed = 1000;
                        Show1.ScrollingStep = 3;
                    }
                    break;
                case "ScrollDown":
                    Show1.TransitionType = OboutInc.Show.TransitionType.Scrolling;
                    Show1.ScrollDirection = OboutInc.Show.ScrollDirection.Bottom;
                    try
                    {
                        XmlElement xmlEffect = (XmlElement)xmlSlConfig.SelectSingleNode("configuration/Effects/ScrollDown");
                        Show1.ScrollingSpeed = Convert.ToInt32(xmlEffect.GetAttribute("ScrollingSpeed"));
                        Show1.ScrollingStep = Convert.ToInt32(xmlEffect.GetAttribute("ScrollingStep"));
                    }
                    catch
                    {
                        Show1.ScrollingSpeed = 1000;
                        Show1.ScrollingStep = 3;
                    }
                    break;
            }
            DataObjectList<DataObjectPicture> slides = DataObjects.Load<DataObjectPicture>(new QuickParameters { RelationParams = new RelationParams { ParentObjectID = objSlideShow.ObjectID }, ObjectType = Helper.GetObjectTypeNumericID("Picture"), ShowState = ObjectShowState.Published, Amount = 0, Direction = QuickSortDirection.Asc, PageNumber = 0, PageSize = 999999, SortBy = QuickSort.RelationSortNumber, Udc = udc });
            if (slides.Count > 0)
            {
                string sTemplate = string.Empty;
                try
                {
                    XmlElement xmlSlideShowTemplate = ((XmlElement)xmlSlConfig.SelectSingleNode(string.Format("configuration/SlideShowTemplate/Community[@ID='{0}']", objSlideShow.CommunityID.Value)));
                    if (xmlSlideShowTemplate != null)
                    {
                        sTemplate = xmlSlideShowTemplate.InnerText;
                    }
                    else
                    {
                        xmlSlideShowTemplate = ((XmlElement)xmlSlConfig.SelectSingleNode("configuration/SlideShowTemplate/Community[@ID='*']"));
                        sTemplate = xmlSlideShowTemplate.InnerText;
                    }
                }
                catch
                {
                    sTemplate = "<table><tr><td><img src='##SLImage##' alt='##SLTitle##' /></td></tr><tr><td><div class='SLDescription'>##SLDesc##</div></td></tr></table>";
                }
                foreach (DataObjectPicture slide in slides)
                {
                    string strImgTitle = slide.Title;
                    string strImgLg = slide.GetImage(PictureVersion.L);
                    string strImgSm = slide.GetImage(PictureVersion.S);
                    StringBuilder tpl = new StringBuilder();
                    tpl.Append(sTemplate);
                    tpl.Replace("##SLImage##", string.Format("{0}{1}", SiteConfig.MediaDomainName, strImgLg));
                    tpl.Replace("##SLTitle##", strImgTitle);
                    tpl.Replace("##SLDesc##", strImgTitle);
                    Show1.AddHtmlPanel(tpl.ToString());
                }
                try
                {
                    XmlElement xmlShow = (XmlElement)xmlSlConfig.SelectSingleNode("configuration/Show");
                    Show1.Width = xmlShow.GetAttribute("Width") + "px";
                    Show1.Height = xmlShow.GetAttribute("Height") + "px";
                }
                catch
                {
                    Show1.Width = "450px";
                    Show1.Height = "500px";
                }
                try
                {
                    XmlElement xmlStyleFolder = (XmlElement)xmlSlConfig.SelectSingleNode(string.Format("configuration/StyleFolder/Community[@ID='{0}']", CtyId));
                    if (xmlStyleFolder != null)
                    {
                        Show1.StyleFolder = string.Format("{0}", xmlStyleFolder.InnerText);
                    }
                    else
                    {
                        xmlStyleFolder = ((XmlElement)xmlSlConfig.SelectSingleNode("configuration/StyleFolder/Community[@ID='*']"));
                        Show1.StyleFolder = string.Format("{0}", xmlStyleFolder.InnerText);
                    }
                }
                catch
                {
                    Show1.StyleFolder = string.Format("/Library/OboutControls/show/styles/style1");
                }

                Show1.Changer.Type = OboutInc.Show.ChangerType.Both;
                Show1.Changer.ArrowType = OboutInc.Show.ArrowType.Side1;
                try
                {
                    XmlElement xmlChanger = (XmlElement)xmlSlConfig.SelectSingleNode("configuration/Changer");
                    Show1.Changer.Height = Convert.ToInt32(xmlChanger.GetAttribute("Height"));
                    Show1.Changer.Width = Convert.ToInt32(xmlChanger.GetAttribute("Width"));
                    if (xmlChanger.GetAttribute("Status").ToLower() == "on")
                    {
                        Show1.ManualChanger = true;
                    }
                    else
                    {
                        Show1.ManualChanger = false;
                    }
                }
                catch
                {
                    Show1.Changer.Height = 40;
                    Show1.Changer.Width = 200;
                    Show1.ManualChanger = true;
                }
                litCtrl.Text = string.Empty;
                litCtrl.Visible = false;
            }
            else
            {
                Show1.Visible = false;
                litCtrl.Text = GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base").GetString("MessageSlideShowNoPicture");
                litCtrl.Visible = true;
            }
        }

    }
}