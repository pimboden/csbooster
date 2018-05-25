// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class EmbedCode : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        private string embedCode = string.Empty;
        public DataObject DataObject { get; set; }
        public string FolderParams { get; set; }
        public QuickParameters QuickParameters { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected string EmbedingCode
        {
            get
            {
                return embedCode.Replace("\"", "&quot;");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DataObject.ObjectType == Helper.GetObjectTypeNumericID("Picture"))
            {
                if (!(DataObject is DataObjectPicture))
                    DataObject = DataObject.Load<DataObjectPicture>(DataObject.ObjectID.Value);

                if (DataObject.State != ObjectState.Added)
                    embedCode = string.Format("<a href=\"{0}{1}\"><img src=\"{2}{3}\" alt=\"{4} von {5}\"></a>", SiteConfig.SiteURL, Helper.GetDetailLink(DataObject.ObjectType, DataObject.ObjectID.Value.ToString()), SiteConfig.MediaDomainName, ((DataObjectPicture)DataObject).GetImage(PictureVersion.L), DataObject.Title, DataObject.Nickname);
            }
            else if (DataObject.ObjectType == Helper.GetObjectTypeNumericID("Video"))
            {
                if (!(DataObject is DataObjectVideo))
                    DataObject = DataObject.Load<DataObjectVideo>(DataObject.ObjectID.Value);

                int embedVideoWidth = 400;
                if (Settings.ContainsKey("EmbedVideoWidth"))
                    embedVideoWidth = (int)Settings["EmbedVideoWidth"];

                int embedVideoHeight = 300;
                if (Settings.ContainsKey("EmbedVideoHeight"))
                    embedVideoHeight = (int)Settings["EmbedVideoHeight"];

                // TODO: implement Flashvars lockup from Themes      <embed src='http://localhost/CSBooster.WebUI2/Library/Scripts/FlashPlayer/player.swf' height='288' width='512' bgcolor='0x333333' allowscriptaccess='always' allowfullscreen='true' flashvars='type=video&lightcolor=0x000000&skin=http%3A%2F%2Flocalhost%2Fcsbooster.webui2%2FApp_Themes%2FSahara%2Fvideoskin.zip&backcolor=0x333333&image=http%3A%2F%2Flocalhost%3A84%2F7b1be42f-3306-4f90-a2d7-ae9690bea7df%2FP%2FL%2Fbc4afa2f-1759-4cc9-afbc-c466db6025db.jpg&frontcolor=0xffff00&screencolor=0x333333&controlbar=over&file=http%3A%2F%2Flocalhost%3A84%2F7b1be42f-3306-4f90-a2d7-ae9690bea7df%2Fv%2Fflv%2Fbc4afa2f-1759-4cc9-afbc-c466db6025db.flv&viral.functions=embed&plugins=viral-2'/>
                if (DataObject.State != ObjectState.Added)
                    embedCode = string.Format("<embed src=\"{0}/Library/Scripts/FlashPlayer/player.swf\" wmode=\"transparent\" type=\"application/x-shockwave-flash\" allowscriptaccess=\"always\" allowfullscreen=\"true\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" flashvars=\"file={4}{2}&image={1}{3}&link={0}{7}\" width=\"{5}\" height=\"{6}\">", SiteConfig.SiteURL, SiteConfig.MediaDomainName, ((DataObjectVideo)DataObject).GetLocation(VideoFormat.Flv, VideoVersion.None), ((DataObjectVideo)DataObject).GetImage(PictureVersion.L), Helper.GetVideoBaseURL(), embedVideoWidth.ToString(), embedVideoHeight.ToString(), Helper.GetDetailLink(DataObject.ObjectType, DataObject.ObjectID.Value.ToString()));
            }
            else
            {
                this.Visible = false;
            }
        }
    }
}
