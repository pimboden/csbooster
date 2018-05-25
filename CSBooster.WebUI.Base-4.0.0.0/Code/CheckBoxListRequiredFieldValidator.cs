using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using _4screen.CSB.Common;  

using System.Web.UI.WebControls;

namespace _4screen.CSB.WebUI.Code
{
    public class CheckBoxListRequiredFieldValidator : BaseValidator
    {
        protected override bool ControlPropertiesValid()
        { 
            return true;
        } 

        protected override bool EvaluateIsValid()
        {
            return this.EvaluateIsChecked();
        }

        protected bool EvaluateIsChecked()
        { 
            CheckBoxList _cbl = ((CheckBoxList)Helper.FindControl(this, this.ControlToValidate));
            foreach (ListItem li in _cbl.Items)
            {
                if (li.Selected == true)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.EnableClientScript)
            {
                this.ClientScript(); 
            }
            base.OnPreRender(e);
        }

        protected void ClientScript()
        {
            this.Attributes["evaluationfunction"] = "cb_vefify";
            StringBuilder sb_Script = new StringBuilder();
            sb_Script.Append("<script language=\"javascript\">");
            sb_Script.Append("\r");
            sb_Script.Append("\r");
            sb_Script.Append("function cb_vefify(val) {");
            sb_Script.Append("\r");
            sb_Script.Append("var val = document.all[document.all[\"");
            sb_Script.Append(this.ClientID);
            sb_Script.Append("\"].controltovalidate];");
            sb_Script.Append("\r");
            sb_Script.Append("var col = val.all;");
            sb_Script.Append("\r");
            sb_Script.Append("if ( col != null ) {");
            sb_Script.Append("\r");
            sb_Script.Append("for ( i = 0; i < col.length; i++ ) {");
            sb_Script.Append("\r");
            sb_Script.Append("if (col.item(i).tagName == \"INPUT\") {");
            sb_Script.Append("\r");
            sb_Script.Append("if ( col.item(i).checked ) {");
            sb_Script.Append("\r");
            sb_Script.Append("\r");
            sb_Script.Append("return true;");
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("\r");
            sb_Script.Append("}"); 
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("\r");
            sb_Script.Append("\r");
            sb_Script.Append("\r");
            sb_Script.Append("return false;");
            sb_Script.Append("\r"); 
            sb_Script.Append("}"); 
            sb_Script.Append("\r");
            sb_Script.Append("}");
            sb_Script.Append("\r");
            sb_Script.Append("</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(String), "RBLScript", sb_Script.ToString());
        }
    }
}
