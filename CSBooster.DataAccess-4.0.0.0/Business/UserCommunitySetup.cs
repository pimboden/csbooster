// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data;
using _4screen.CSB.Common;
using SubSonic;

namespace _4screen.CSB.DataAccess.Business
{
    public class UserCommunitySetup
    {
        public UserCommunitySetup(Guid UserId, string LangCode)
        {
            _UserId = UserId;
            _CurrLang = LangCode;
            _CurrentPage = null;
        }

        private string _CurrLang;

        public string CurrLang
        {
            get { return _CurrLang; }
            set { _CurrLang = value; }
        }


        private Guid _UserId;

        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private HitblUserSettingsUst _UserSettings;

        public HitblUserSettingsUst UserSettings
        {
            get
            {
                if (_UserSettings == null)
                {
                    GetUserSettings();
                }

                return _UserSettings;
            }
            set { _UserSettings = value; }
        }

        private void GetUserSettings()
        {
            if (_CurrentCommunity != null)
            {
                Query qry = new Query(HitblUserSettingsUst.Schema);
                qry.AddWhere(HitblUserSettingsUst.Columns.UsrCurrentCommunityId, Comparison.Equals, _CurrentCommunity.CtyId);
                qry.AddWhere(HitblUserSettingsUst.Columns.UsrId, Comparison.Equals, _UserId);

                IDataReader idr = null;
                try
                {
                    idr = qry.ExecuteReader();
                    HitblUserSettingsUstCollection UserSettingsUSTCollection = new HitblUserSettingsUstCollection();
                    UserSettingsUSTCollection.Load(idr);
                    idr.Close();
                    if (UserSettingsUSTCollection.Count > 0)
                    {
                        _UserSettings = (HitblUserSettingsUst)UserSettingsUSTCollection[0];
                    }
                    else
                    {
                        _UserSettings = new HitblUserSettingsUst();
                        _UserSettings.UsrCurrentCommunityId = _CurrentCommunity.CtyId;
                        _UserSettings.UsrCurrentLang = _CurrLang;
                        _UserSettings.UsrId = _UserId;
                        if (_CurrentPage != null)
                        {
                            _UserSettings.UsrCurrentPageId = _CurrentPage.PagId;
                        }
                        _UserSettings.Save();
                    }
                }
                finally
                {
                    if (idr != null && !idr.IsClosed)
                    {
                        idr.Close();
                    }
                }
            }
        }


        private VieverType _AccessRight;

        public VieverType AccesRight
        {
            get { return _AccessRight; }
            set { _AccessRight = value; }
        }

        private HitblCommunityCty _CurrentCommunity;

        public HitblCommunityCty CurrentCommunity
        {
            get { return _CurrentCommunity; }
            set { _CurrentCommunity = value; }
        }

        private HitblPagePag _CurrentPage;

        public HitblPagePag CurrentPage
        {
            get
            {
                if (_CurrentPage == null && Pages.Count > 0)
                {
                    _CurrentPage = Pages[0];
                }
                return _CurrentPage;
            }
            set { _CurrentPage = value; }
        }

        private HitblPagePagCollection _Pages;

        public HitblPagePagCollection Pages
        {
            get
            {
                if (_Pages == null && _CurrentCommunity != null)
                {
                    _Pages = new HitblPagePagCollection();
                    IDataReader idr = null;
                    try
                    {
                        idr = HitblPagePag.FetchByParameter(HitblPagePag.Columns.CtyId, SubSonic.Comparison.Equals, _CurrentCommunity.CtyId, SubSonic.OrderBy.Asc(HitblPagePag.Columns.PagOrderNr));

                        _Pages.Load(idr);
                        idr.Close();
                        DataObject doComm = DataObject.Load<DataObject>(_CurrentCommunity.CtyId);
                        if (doComm.State != ObjectState.Added)
                        {
                            if (doComm.ObjectType == Helper.GetObjectType("ProfileCommunity").NumericId && UserId != doComm.UserID.Value)
                            {
                                _Pages.RemoveAt(0);
                            }
                            else if (doComm.ObjectType == Helper.GetObjectType("Community").NumericId)
                            {
                                bool isOwner = false;
                                bool isMember = false;
                                isOwner = Community.GetIsUserOwner(UserId, _CurrentCommunity.CtyId, out isMember);
                                if (!isOwner && _Pages.Count > 1)
                                    _Pages.RemoveAt(0);
                            }
                        }
                    }
                    finally
                    {
                        if (idr != null && !idr.IsClosed)
                        {
                            idr.Close();
                        }
                    }
                }

                return _Pages;
            }
            set { _Pages = value; }
        }

        private HitblWidgetInstanceInCollection _WidgetInstances;

        public HitblWidgetInstanceInCollection WidgetInstances
        {
            get
            {
                _WidgetInstances = new HitblWidgetInstanceInCollection();
                Query qry = new Query(HitblWidgetInstanceIn.Schema);
                qry.AddWhere(HitblWidgetInstanceIn.Columns.InsPagId, Comparison.Equals, CurrentPage.PagId);
                qry.OrderBy = OrderBy.Asc(HitblWidgetInstanceIn.Columns.InsOrderNo);
                IDataReader idr = null;
                try
                {
                    idr = qry.ExecuteReader();
                    _WidgetInstances.Load(idr);
                }
                finally
                {
                    if (idr != null && !idr.IsClosed)
                    {
                        idr.Close();
                    }
                }
                return _WidgetInstances;
            }
            set { _WidgetInstances = value; }
        }
    }
}