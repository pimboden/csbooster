using System.Xml;
using _4screen.CSB.DataAccess;

namespace _4screen.CSB.Widget
{
    public partial class KyteVideo : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            bool HasContent = false;
            try
            {
                XmlDocument xmlDom = new XmlDocument();
                xmlDom.LoadXml(settingsXml);

                string strChannel = XmlHelper.GetElementValueCDATA(xmlDom.DocumentElement, "KyteChannel", string.Empty);
                PrintKyteTV(strChannel);
                HasContent = !string.IsNullOrEmpty(strChannel);
            }
            catch
            {
            }
            return HasContent;
        }

        private void PrintKyteTV(string channelNr)
        {
            string strKyte = @"    <div style='padding: 0px 0px 0px 0px; margin: 0'>
         <embed width='425' height='500' wmode='transparent' type='application/x-shockwave-flash' allowScriptAccess='always' allowFullScreen='true' 
         style='display:block;margin:0' src='http://www.kyte.tv/flash.swf?appKey=MarbachViewerEmbedded&uri=channels/##CHANNEL##&embedId=14551309&premium=true' bgcolor='#333333'></embed>
        <div style='padding: 0; margin: 0'>
         <embed type='application/x-shockwave-flash' allowScriptAccess='always' style='display:block;margin:0' width='425' height='20' 
         src='http://media01.kyte.tv/images/updatenotice.swf' flashvars='requiredversion=9.0.28' wmode='transparent'></embed>
            </div>
         </div>";

            litKyte.Text = strKyte.Replace("##CHANNEL##", channelNr);
        }
    }
}