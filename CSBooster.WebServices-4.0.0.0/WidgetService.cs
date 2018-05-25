// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebServices
{
    [WebService(Namespace = "http://www.4screen.ch/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class WidgetService : System.Web.Services.WebService
    {
        public WidgetService()
        {
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public void MoveWidgetInstance(string widgetInstanceId, int toColum, int toRow)
        {
            Guid communityId = DataAccess.Business.Utils.GetCommunityIdFromWidgetInstance(widgetInstanceId.ToGuid());
            if ((DataObject.GetUserAccess(UserDataContext.GetUserDataContext(), communityId, communityId, Common.Helper.GetObjectTypeNumericID("Community")) & ObjectAccessRight.Update) == ObjectAccessRight.Update)
            {
                SPs.HispMoveWidgetInstance(new Guid(widgetInstanceId), toColum, toRow).Execute();
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public void AddWidgetInstance(string widgetId, string pageId, int column, int row)
        {
            if (ActionValidator.IsValid(ActionValidator.ActionTypeEnum.AddNewWidget))
            {
                Guid communityId = DataAccess.Business.Utils.GetCommunityIdFromPage(pageId.ToGuid());
                if ((DataObject.GetUserAccess(UserDataContext.GetUserDataContext(), communityId, communityId, Common.Helper.GetObjectTypeNumericID("Community")) & ObjectAccessRight.Update) == ObjectAccessRight.Update)
                {
                    WidgetElement widget = WidgetSection.CachedInstance.Widgets.Cast<WidgetElement>().Where(w => w.Id == widgetId.ToGuid()).Single();

                    HitblWidgetInstanceIn widgetInstance = new HitblWidgetInstanceIn();
                    widgetInstance.InsId = Guid.NewGuid();
                    widgetInstance.InsColumnNo = column;
                    widgetInstance.InsCreatedDate = widgetInstance.InsLastUpdate = DateTime.Now;
                    widgetInstance.InsShowAfterInsert = (int)widget.ShowAfterInsert;
                    widgetInstance.InsOrderNo = row;
                    widgetInstance.InsPagId = new Guid(pageId);
                    widgetInstance.InsXmlStateData = widget.Settings.Value;
                    widgetInstance.WdgId = widgetId.ToGuid();
                    widgetInstance.WtpId = Constants.DEFAULT_LAYOUTID.ToGuid();
                    widgetInstance.InsHideIfNoContent = true;
                    if (!string.IsNullOrEmpty(widget.OutputTemplates))
                    {
                        string[] outputTemplates = widget.OutputTemplates.Split(';');
                        widgetInstance.InsOutputTemplate = outputTemplates[0].ToGuid();
                    }
                    widgetInstance.Save();

                    HitblWidgetInstanceTextWit widgetInstanceText = new HitblWidgetInstanceTextWit();
                    widgetInstanceText.InsId = widgetInstance.InsId;
                    widgetInstanceText.WitLangCode = "de-CH";
                    widgetInstanceText.WitTitle = GuiLanguage.GetGuiLanguage(widget.LocalizationBaseFileName).GetString(widget.TitleKey);
                    widgetInstanceText.Save();

                    SPs.HispWidgetInstanceReorderByPageColumn(new Guid(pageId), widgetInstance.InsColumnNo).Execute();
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public void AddWidgetInstanceByObjectType(string objectId, string objectType, string pageId, int column, int row)
        {
            if (ActionValidator.IsValid(ActionValidator.ActionTypeEnum.AddNewWidget))
            {
                Guid communityId = DataAccess.Business.Utils.GetCommunityIdFromPage(pageId.ToGuid());
                if ((DataObject.GetUserAccess(UserDataContext.GetUserDataContext(), communityId, communityId, Common.Helper.GetObjectTypeNumericID("Community")) & ObjectAccessRight.Update) == ObjectAccessRight.Update)
                {
                    Guid widgetId = Common.Helper.GetObjectType(objectType).DetailWidgetId;
                    WidgetElement widget = WidgetSection.CachedInstance.Widgets.Cast<WidgetElement>().Where(w => w.Id == widgetId).Single();

                    HitblWidgetInstanceIn widgetInstance = new HitblWidgetInstanceIn();
                    widgetInstance.InsId = Guid.NewGuid();
                    widgetInstance.InsColumnNo = column;
                    widgetInstance.InsCreatedDate = widgetInstance.InsLastUpdate = DateTime.Now;
                    widgetInstance.InsShowAfterInsert = 0;
                    widgetInstance.InsOrderNo = row;
                    widgetInstance.InsPagId = new Guid(pageId);
                    widgetInstance.InsXmlStateData = string.Format("<root><ObjectType>{0}</ObjectType><ByUrl>False</ByUrl><Source>-1</Source><ObjectID>{1}</ObjectID></root>", Common.Helper.GetObjectType(objectType).Id, objectId);
                    widgetInstance.WdgId = widgetId;
                    widgetInstance.WtpId = Constants.DEFAULT_LAYOUTID.ToGuid();
                    widgetInstance.InsHideIfNoContent = true;
                    if (!string.IsNullOrEmpty(widget.OutputTemplates))
                    {
                        string[] outputTemplates = widget.OutputTemplates.Split(';');
                        widgetInstance.InsOutputTemplate = outputTemplates[0].ToGuid();
                    }
                    widgetInstance.Save();

                    HitblWidgetInstanceTextWit widgetInstanceText = new HitblWidgetInstanceTextWit();
                    widgetInstanceText.InsId = widgetInstance.InsId;
                    widgetInstanceText.WitLangCode = "de-CH";
                    widgetInstanceText.WitTitle = "##OBJ_TITLE##";
                    widgetInstanceText.Save();

                    SPs.HispWidgetInstanceReorderByPageColumn(new Guid(pageId), widgetInstance.InsColumnNo).Execute();
                }
            }
        }
    }
}