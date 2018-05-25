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
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class AdminLinksSettings_Step10 : StepsASCX
    {
        #region FIELDS

        private Guid InstanceID;
        private Control ctrlRoleVisibilityAndFixation = null;
        private IRoleVisibilityAndFixation RoleVisibilityAndFixation = null;

        #endregion FIELDS

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InstanceID = new Guid(ObjectID);
            ctrlRoleVisibilityAndFixation = LoadControl("~/WidgetControls/RoleVisibilityAndFixation.ascx");
            RoleVisibilityAndFixation = (IRoleVisibilityAndFixation)ctrlRoleVisibilityAndFixation;
            ctrlRoleVisibilityAndFixation.ID = "RVAF";
            RoleVisibilityAndFixation.InstanceID = InstanceID;
            phRF.Controls.Add(ctrlRoleVisibilityAndFixation);
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
                    RoleVisibilityAndFixation.Save();
                    return true;
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

        #endregion PRIVATE METHODES
    }
}