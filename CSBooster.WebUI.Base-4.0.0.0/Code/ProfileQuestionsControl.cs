//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.Code
{
    public class ProfileQuestionsControl : System.Web.UI.UserControl
    {
        public virtual void FillCheckBoxListAnswer(CheckBoxList cbl, string answer, FillListBy FilledBy)
        {
            //FillCheckBoxListAnswer -- may only be stored in a string;
            if (!string.IsNullOrEmpty(answer))
            {
                if (FilledBy == FillListBy.Any)
                {
                    foreach (ListItem li in cbl.Items)
                    {
                        if (answer.IndexOf(string.Format(";{0};", li.Value)) > -1 || answer.IndexOf(string.Format(";{0};", li.Text)) > -1)
                        {
                            li.Selected = true;
                        }
                        else
                        {
                            li.Selected = false;
                        }
                        PrintExtension(li, answer);
                    }
                }

                else if (FilledBy == FillListBy.Value)
                {
                    foreach (ListItem li in cbl.Items)
                    {
                        if (answer.IndexOf(string.Format(";{0};", li.Value)) > -1)
                        {
                            li.Selected = true;
                        }
                        else
                        {
                            li.Selected = false;
                        }
                        PrintExtension(li, answer);
                    }
                }
                else
                {
                    //Text
                    foreach (ListItem li in cbl.Items)
                    {
                        if (answer.IndexOf(string.Format(";{0};", li.Text)) > -1)
                        {
                            li.Selected = true;
                        }
                        else
                        {
                            li.Selected = false;
                        }
                        PrintExtension(li, answer);
                    }
                }
            }
        }

        public virtual void FillDropDownListAnswer(DropDownList ddl, string answer, FillListBy FilledBy)
        {
            //FillDropDownListAnswer -- may only be stored in a string;
            if (!string.IsNullOrEmpty(answer))
            {
                if (FilledBy == FillListBy.Any)
                {
                    foreach (ListItem li in ddl.Items)
                    {
                        if (answer.IndexOf(string.Format(";{0};", li.Value)) > -1 || answer.IndexOf(string.Format(";{0};", li.Text)) > -1)
                        {
                            li.Selected = true;
                        }
                        else
                        {
                            li.Selected = false;
                        }
                    }
                }
                else if (FilledBy == FillListBy.Value)
                {
                    foreach (ListItem li in ddl.Items)
                    {
                        if (answer.IndexOf(string.Format(";{0};", li.Value)) > -1)
                        {
                            li.Selected = true;
                        }
                        else
                        {
                            li.Selected = false;
                        }
                    }
                }
                else
                {
                    foreach (ListItem li in ddl.Items)
                    {
                        if (answer.IndexOf(string.Format(";{0};", li.Text)) > -1)
                        {
                            li.Selected = true;
                        }
                        else
                        {
                            li.Selected = false;
                        }
                    }
                }
            }
        }

        public virtual void FillRadMaskedTextBoxAnswer(RadMaskedTextBox rmtxt, object answer, AnswerType typeOfAnswer)
        {
            if (answer != null)
            {
                switch (typeOfAnswer)
                {
                    case AnswerType.String:
                        rmtxt.Text = answer.ToString();
                        break;
                    case AnswerType.Date:
                        rmtxt.Text = Convert.ToDateTime(answer).ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                        break;
                    case AnswerType.Float:
                        rmtxt.Text = Convert.ToDouble(answer).ToString("2f");
                        break;
                    case AnswerType.Int:
                        rmtxt.Text = answer.ToString();
                        break;
                    default:
                        rmtxt.Text = string.Empty;
                        break;
                }
            }
        }

        public virtual void FillRadDateTimePickerAnswer(Telerik.Web.UI.RadDateTimePicker rdtp, DateTime? answer)
        {
            if (answer != null)
            {
                try
                {
                    rdtp.SelectedDate = answer.Value;
                }
                catch
                {
                    rdtp.SelectedDate = DateTime.Now;
                }
            }
        }

        public virtual void FillRadTimePickerAnswer(Telerik.Web.UI.RadTimePicker rtp, DateTime? answer)
        {
            if (answer != null)
            {
                try
                {
                    rtp.SelectedDate = answer.Value;
                }
                catch
                {
                    rtp.SelectedDate = DateTime.Now;
                }
            }
        }

        public virtual void FillRadDatePickerAnswer(Telerik.Web.UI.RadDatePicker rdp, DateTime? answer)
        {
            if (answer != null)
            {
                try
                {
                    rdp.SelectedDate = answer.Value;
                }
                catch
                {
                    rdp.SelectedDate = DateTime.Now;
                }
            }
            else
            {
                rdp.SelectedDate = null;
            }
        }

        public virtual void FillRadDatePickerAnswer(Telerik.Web.UI.RadDateInput rdp, DateTime? answer)
        {
            if (answer != null)
            {
                try
                {
                    rdp.SelectedDate = answer.Value;
                }
                catch
                {
                    rdp.SelectedDate = DateTime.Now;
                }
            }
        }

        public virtual void FillRadioListAnswer(RadioButtonList rbl, string answer, FillListBy FilledBy)
        {
            if (!string.IsNullOrEmpty(answer))
            {
                if (FilledBy == FillListBy.Any)
                {
                    foreach (ListItem li in rbl.Items)
                    {
                        if (answer.IndexOf(string.Format(";{0};", li.Value)) > -1 || answer.IndexOf(string.Format(";{0};", li.Text)) > -1)
                        {
                            li.Selected = true;
                        }
                        else
                        {
                            li.Selected = false;
                        }
                        PrintExtension(li, answer);
                    }
                }
                else if (FilledBy == FillListBy.Value)
                {
                    foreach (ListItem li in rbl.Items)
                    {
                        if (answer.IndexOf(string.Format(";{0};", li.Value)) > -1)
                        {
                            li.Selected = true;
                        }
                        else
                        {
                            li.Selected = false;
                        }
                        PrintExtension(li, answer);
                    }
                }
                else
                {
                    foreach (ListItem li in rbl.Items)
                    {
                        if (answer.IndexOf(string.Format(";{0};", li.Text)) > -1)
                        {
                            li.Selected = true;
                        }
                        else
                        {
                            li.Selected = false;
                        }
                        PrintExtension(li, answer);
                    }
                }
            }
        }

        public virtual void FillTextboxAnswer(TextBox txtBox, object answer, AnswerType typeOfAnswer)
        {
            if (answer != null)
            {
                switch (typeOfAnswer)
                {
                    case AnswerType.String:
                        txtBox.Text = answer.ToString();
                        break;
                    case AnswerType.Date:
                        txtBox.Text = Convert.ToDateTime(answer).ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                        break;
                    case AnswerType.Float:
                        txtBox.Text = Convert.ToDouble(answer).ToString("2f");
                        break;
                    case AnswerType.Int:
                        txtBox.Text = answer.ToString();
                        break;
                    default:
                        txtBox.Text = string.Empty;
                        break;
                }
            }
        }

        public virtual void PrintExtension(ListItem li, string answer)
        {
            if (!string.IsNullOrEmpty(li.Attributes["WT"]))
            {
                //a textbox must be rendered
                string strValue = string.Empty;
                int startIndex = answer.IndexOf(string.Format(";{0}TxtVal=", li.Attributes["WT"]));
                if (startIndex > -1)
                {
                    string strTemp = string.Format(";{0}TxtVal=", li.Attributes["WT"]);

                    strValue = answer.Substring(startIndex + strTemp.Length);
                    strValue = strValue.Substring(0, strValue.IndexOf(';'));
                }
                if (string.IsNullOrEmpty(li.Attributes["Multiline"]) || li.Attributes["Multiline"].ToLower() == "false")
                {
                    li.Text = li.Text + string.Format(" <input type ='text' id='{0}' name ='{0}' value ='{1}' />", li.Attributes["WT"], strValue);
                }
                else
                {
                    li.Text = li.Text + string.Format(" <textarea id='{0}' cols='50' name='{0}' rows='5'>{1}</textarea>", li.Attributes["WT"], strValue);
                }
                if (strValue.Length > 0)
                {
                    li.Selected = true;
                }
            }
        }

        public virtual string GetComaSeparatetdValues(ListItemCollection Items)
        {
            string strRetval = string.Empty;
            foreach (ListItem li in Items)
            {
                if (li.Selected)
                {
                    strRetval += string.Format("{0};", li.Value);
                    if (!string.IsNullOrEmpty(li.Attributes["WT"]))
                    {
                        strRetval += string.Format("{0}TxtVal={1};", li.Attributes["WT"], Request.Form[li.Attributes["WT"]]);
                    }
                }
            }
            if (strRetval.Length > 0)
                strRetval = ";" + strRetval;
            return strRetval;
        }


    }
}