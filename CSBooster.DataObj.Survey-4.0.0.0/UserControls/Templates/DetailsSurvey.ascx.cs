// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataObj.Data;
using _4screen.CSB.DataObj.SurveyCommon;
using _4screen.CSB.Extensions.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using Telerik.Web.UI;
using DataObjectSurvey=_4screen.CSB.DataObj.Business.DataObjectSurvey;

namespace _4screen.CSB.DataObj.UserControls.Templates
{
    public partial class DetailsSurvey : UserControl, ISettings, IDataObjectWorker
    {
        public DataAccess.Business.DataObject DataObject { get; set; }

        private DataObjectSurvey dataObjectSurvey;
        public Dictionary<string, object> Settings { get; set; }
        private SurveyDataClassDataContext surveyDataClassDataContext;
        private int countSurveyPages;
        public Dictionary<string, Control> AnswerControls { get; set; }
        private int totalTestQuestions;
        private int currPage;
        private List<string> surveyPagesTitle;
        private bool isUserAdminOrOwner = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            totalTestQuestions = 0;
            surveyPagesTitle = new List<string>();
            if (!int.TryParse(Request.Form[hdCurrPage.UniqueID], out currPage))
                currPage = 0;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            surveyDataClassDataContext = new SurveyDataClassDataContext();
            try
            {
                if (DataObject is Business.DataObjectSurvey)
                    dataObjectSurvey = (Business.DataObjectSurvey)DataObject;
                else
                    dataObjectSurvey = DataAccess.Business.DataObject.Load<Business.DataObjectSurvey>(DataObject.ObjectID, null, false);


                //Check the Container of the Detail
                if (Settings.ContainsKey("ParentObjectType"))
                {
                    int parentObjectType = (int)Settings["ParentObjectType"];
                    if (parentObjectType == Helper.GetObjectTypeNumericID("Page"))
                    {
                        isUserAdminOrOwner = UserDataContext.GetUserDataContext().IsAdmin;
                    }
                    else
                    {
                        Community comm = new Community((Guid)Settings["ParentCommunityID"]);
                        isUserAdminOrOwner = comm.IsUserOwner || UserDataContext.GetUserDataContext().IsAdmin;
                    }
                }

                AnswerControls = new Dictionary<string, Control>();
                if (dataObjectSurvey != null && dataObjectSurvey.State != ObjectState.Added)
                {
                    PrintOutput();
                }

            }
            catch
            {
            }

            radMP.SelectedIndex = currPage;
        }

        private void PrintOutput()
        {
            DateTime dt = DateTime.Now;
            bool cntVisible = true;
            if (dt < dataObjectSurvey.StartDate || dt > dataObjectSurvey.EndDate || dataObjectSurvey.ShowState != ObjectShowState.Published)
            {
                cntVisible = false;
            }
            if (cntVisible || isUserAdminOrOwner)
            {
                if (cntVisible)
                {
                    dataObjectSurvey.AddViewed(UserDataContext.GetUserDataContext());

                    if (UserProfile.Current.UserId != dataObjectSurvey.UserID.Value)
                    {
                        IncentivePointsManager.AddIncentivePointEvent(
                            dataObjectSurvey.ObjectType.ToString().ToUpper() + "_VIEWED", UserDataContext.GetUserDataContext(),
                            dataObjectSurvey.ObjectID.Value.ToString());
                    }
                }
                if (isUserAdminOrOwner)
                {

                    //Admin will see a special div
                    if (!cntVisible)
                        pnlOut.CssClass = "CSB_CNT_public_invisible";
                    else
                        pnlOut.CssClass = "CSB_CNT_public_OK";
                }
                RTTM.Visible = false;
                if (dataObjectSurvey != null)
                {
                    foreach (var tooltipId in AdWordHelper.GetCampaignObjectIds(dataObjectSurvey.ObjectID.Value))
                    {
                        RTTM.TargetControls.Add(tooltipId, true);
                        RTTM.Visible = true;
                    }
                }
                litHeader.Text = string.Format("<div class='CSB_Survey_Header'>{0}</div>", dataObjectSurvey.HeaderLinked);
                litFooter.Text = string.Format("<div class='CSB_Survey_Footer'>{0}</div>", dataObjectSurvey.FooterLinked);
                var surveyPages = from allPages in surveyDataClassDataContext.hitbl_Survey_Page_SPGs.Where(x => x.OBJ_ID == dataObjectSurvey.ObjectID.Value)
                                  orderby allPages.SortNumber ascending
                                  select allPages;
                countSurveyPages = surveyPages.Count();

                foreach (hitbl_Survey_Page_SPG surveyPage in surveyPages)
                {
                    surveyPagesTitle.Add(surveyPage.Title);
                    RadPageView rpv = new RadPageView();
                    rpv.ID = string.Format("rpv{0}", surveyPage.SPG_ID);
                    rpv.Controls.Add(PrintQuestionAndAnswers(surveyPage));
                    radMP.PageViews.Add(rpv);
                }
                AddPager();
            }
        }

        private void AddPager()
        {
            Panel pnlPager = new Panel
                                 {
                                     CssClass = "SurveyPager"
                                 };
            pager.Controls.Clear();
            if (totalTestQuestions > 0)
            {
                if (countSurveyPages == 1)
                {
                    LinkButton lbtnFinish = new LinkButton
                                                {
                                                    ID = "PF",
                                                    Text = !dataObjectSurvey.IsContest ? GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("SurveyFinishButtonText") : GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("SurveyFinishButtonTextContest"),
                                                    CssClass = "SurveyFinish"
                                                };
                    lbtnFinish.Click += new EventHandler(lbtnFinish_Click);
                    pnlPager.Controls.Add(lbtnFinish);
                    pager.Controls.Add(pnlPager);
                }
                else
                {
                    for (int i = 0; i < countSurveyPages; i++)
                    {
                        LinkButton lbtPage = new LinkButton
                                                {
                                                    ID = string.Format("P{0}", i),
                                                    Text = surveyPagesTitle.Skip(i).Take(1).SingleOrDefault(),
                                                    CommandArgument = i.ToString(),
                                                    Enabled = i != currPage,
                                                    CssClass = i != currPage ? "pagea" : "pagei"
                                                };
                        lbtPage.Click += new EventHandler(lbtPage_Click);
                        pnlPager.Controls.Add(lbtPage);
                    }
                    LinkButton lbtnFinish = new LinkButton
                                                {
                                                    ID = "PF",
                                                    Text = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("SurveyFinishButtonText"),
                                                    CssClass = "SurveyFinish"
                                                };
                    lbtnFinish.Click += new EventHandler(lbtnFinish_Click);
                    pnlPager.Controls.Add(lbtnFinish);
                    pager.Controls.Add(pnlPager);
                }
            }
            else
            {
                pager.Visible = false;
            }
            pnlPager.ID = null;
        }

        private Control PrintQuestionAndAnswers(hitbl_Survey_Page_SPG surveyPage)
        {
            Panel pnlPage = new Panel
                                {
                                    ID = string.Format("pnlPage{0}", surveyPage.SPG_ID),
                                    CssClass = "CSB_SurveyDetail_Page"
                                };
            if (!string.IsNullOrEmpty(surveyPage.Description))
            {
                pnlPage.Controls.Add(new LiteralControl(string.Format("<div class='Desc'>{0}</div>", surveyPage.Description)));
            }
            foreach (hitbl_Survey_Question_SQU surveyQuestion in surveyPage.hitbl_Survey_Question_SQUs.OrderBy(x => x.SortNumber))
            {
                if (surveyQuestion.QuestionType != SurveyAnswersType.NotSelected)
                {
                    totalTestQuestions++;
                    pnlPage.Controls.Add(new LiteralControl(string.Format("<div class='CSB_SurveyDetail_Question'>{0}</div>", surveyQuestion.QuestionText)));
                    Control controlAnswer = null;
                    switch (surveyQuestion.QuestionType)
                    {
                        case SurveyAnswersType.SingleTextbox:
                            TextBox txtSingleTextbox = new TextBox
                                                           {
                                                               Width = Unit.Percentage(100),
                                                               ID = string.Format("txt_{0}", surveyQuestion.SQU_ID)
                                                           };
                            controlAnswer = txtSingleTextbox;
                            break;
                        case SurveyAnswersType.Textarea:
                            TextBox txtTextarea = new TextBox
                                                      {
                                                          Width = Unit.Percentage(100),
                                                          ID = string.Format("txtArea_{0}", surveyQuestion.SQU_ID),
                                                          Rows = 2,
                                                          TextMode = TextBoxMode.MultiLine
                                                      };
                            controlAnswer = txtTextarea;
                            break;
                        case SurveyAnswersType.MultipleChoiceMultipleAnswers:
                            CheckBoxList cbl = new CheckBoxList
                                                   {
                                                       ID = string.Format("cbl{0}", surveyQuestion.SQU_ID)
                                                   };
                            foreach (hitbl_Survey_Answer_Row_SAR surveyAnswerRow in surveyQuestion.hitbl_Survey_Answer_Row_SARs.OrderBy(x => x.SortNumber))
                            {
                                cbl.Items.Add(new ListItem(surveyAnswerRow.AnswerText, string.Format("{0}_{1}", surveyAnswerRow.AnswerWeight, surveyAnswerRow.SAR_ID.ToString().Replace("-", ""))));
                            }
                            controlAnswer = cbl;
                            break;
                        case SurveyAnswersType.MultipleChoiceOnlyOneAnswer:
                            RadioButtonList rbl = new RadioButtonList
                                                      {
                                                          ID = string.Format("rbl{0}", surveyQuestion.SQU_ID)
                                                      };
                            foreach (hitbl_Survey_Answer_Row_SAR surveyAnswerRow in surveyQuestion.hitbl_Survey_Answer_Row_SARs.OrderBy(x => x.SortNumber))
                            {
                                rbl.Items.Add(new ListItem(surveyAnswerRow.AnswerText, string.Format("{0}_{1}", surveyAnswerRow.AnswerWeight, surveyAnswerRow.SAR_ID.ToString().Replace("-", ""))));
                            }
                            controlAnswer = rbl;
                            break;
                        default:
                            break;
                    }
                    if (controlAnswer != null)
                    {
                        AnswerControls.Add(controlAnswer.ID, controlAnswer);
                        pnlPage.Controls.Add(new LiteralControl("<div class='CSB_SurveyDetail_Answer'>"));
                        pnlPage.Controls.Add(controlAnswer);
                        pnlPage.Controls.Add(new LiteralControl("</div>"));

                    }
                }
            }
            return pnlPage;
        }

        protected void lbtPage_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            currPage = Convert.ToInt32(lbtn.CommandArgument);
            hdCurrPage.Value = currPage.ToString();
            AddPager();
            radMP.SelectedIndex = currPage;
        }

        private void lbtnFinish_Click(object sender, EventArgs e)
        {
            bool isInputComplete = true;
            StringBuilder sbMailText = new StringBuilder();
            double calculatePoints = 0.0;
            var surveyPages = from allPages in surveyDataClassDataContext.hitbl_Survey_Page_SPGs.Where(x => x.OBJ_ID == dataObjectSurvey.ObjectID.Value)
                              orderby allPages.SortNumber ascending
                              select allPages;
            countSurveyPages = surveyPages.Count();
            string answerKey = string.Empty;
            //Save The Test for The user
            hitbl_Survey_User_Result_SUR hitblSurveyUserResult = new hitbl_Survey_User_Result_SUR
                                                        {
                                                            SUR_ID = Guid.NewGuid(),
                                                            OBJ_ID = dataObjectSurvey.ObjectID.Value,
                                                            TestTitle = dataObjectSurvey.Title,
                                                            TestDate = DateTime.Now,
                                                            Status = 1,
                                                            IsContest = dataObjectSurvey.IsContest
                                                        };
            if (Request.IsAuthenticated && !dataObjectSurvey.ShowForm)
            {
                DataObjectUser dataObjectSurveyUser = DataObject.Load<DataObjectUser>(UserDataContext.GetUserDataContext().UserID);

                if (dataObjectSurveyUser.State != ObjectState.Added)
                {
                    hitblSurveyUserResult.Email = dataObjectSurveyUser.GetMail;
                    hitblSurveyUserResult.Name = dataObjectSurveyUser.Name;
                    hitblSurveyUserResult.Vorname = dataObjectSurveyUser.Vorname;
                    hitblSurveyUserResult.USR_ID = dataObjectSurveyUser.ObjectID.Value;
                }
            }
            surveyDataClassDataContext.hitbl_Survey_User_Result_SURs.InsertOnSubmit(hitblSurveyUserResult);

            foreach (hitbl_Survey_Page_SPG surveyPage in surveyPages)
            {
                foreach (hitbl_Survey_Question_SQU surveyQuestion in surveyPage.hitbl_Survey_Question_SQUs.OrderBy(x => x.SortNumber))
                {
                    sbMailText.AppendFormat("{0} <br/>", surveyQuestion.QuestionText);
                    if (surveyQuestion.QuestionType != SurveyAnswersType.NotSelected && surveyQuestion.QuestionType != SurveyAnswersType.NoAnswers)
                    {
                        hitbl_Survey_User_ResultItem_SUI hitblSurveyUserResultItem = new hitbl_Survey_User_ResultItem_SUI
                                                                 {
                                                                     SUI_ID = Guid.NewGuid(),
                                                                     SQU_ID = surveyQuestion.SQU_ID,
                                                                     QuestionText = surveyQuestion.QuestionText,
                                                                     Answer = string.Empty,
                                                                     AnswerWeight = 0.0
                                                                 };
                        switch (surveyQuestion.QuestionType)
                        {
                            case SurveyAnswersType.SingleTextbox:
                                answerKey = string.Format("txt_{0}", surveyQuestion.SQU_ID);
                                TextBox txtAnswer = AnswerControls[answerKey] as TextBox;
                                if (txtAnswer.Text.Trim().Length > 0)
                                {
                                    try
                                    {
                                        hitblSurveyUserResultItem.AnswerWeight = (surveyQuestion.hitbl_Survey_Answer_Row_SARs[0].AnswerWeight.HasValue ? surveyQuestion.hitbl_Survey_Answer_Row_SARs[0].AnswerWeight.Value : 0.0);
                                        calculatePoints = calculatePoints + hitblSurveyUserResultItem.AnswerWeight.Value;
                                        hitblSurveyUserResultItem.Answer = txtAnswer.Text;
                                        sbMailText.AppendFormat("{0} <br/><hr/>", txtAnswer.Text);
                                    }
                                    catch (Exception exception)
                                    {
                                    }
                                }
                                break;
                            case SurveyAnswersType.Textarea:
                                answerKey = string.Format("txtArea_{0}", surveyQuestion.SQU_ID);
                                TextBox txtAreaAnswer = AnswerControls[answerKey] as TextBox;
                                if (txtAreaAnswer.Text.Trim().Length > 0)
                                {
                                    try
                                    {
                                        hitblSurveyUserResultItem.AnswerWeight = (surveyQuestion.hitbl_Survey_Answer_Row_SARs[0].AnswerWeight.HasValue ? surveyQuestion.hitbl_Survey_Answer_Row_SARs[0].AnswerWeight.Value : 0.0);
                                        calculatePoints = calculatePoints + hitblSurveyUserResultItem.AnswerWeight.Value;
                                        hitblSurveyUserResultItem.Answer = txtAreaAnswer.Text;
                                        sbMailText.AppendFormat("{0} <br/><hr/>", txtAreaAnswer.Text);

                                    }
                                    catch (Exception exception)
                                    {
                                    }
                                }
                                break;
                            case SurveyAnswersType.MultipleChoiceMultipleAnswers:
                                answerKey = string.Format("cbl{0}", surveyQuestion.SQU_ID);
                                CheckBoxList cbl = AnswerControls[answerKey] as CheckBoxList;
                                foreach (ListItem item in cbl.Items)
                                {
                                    if (item.Selected)
                                    {
                                        try
                                        {
                                            hitblSurveyUserResultItem.AnswerWeight = +Convert.ToDouble(item.Value.Split('_')[0]);
                                            calculatePoints = calculatePoints + hitblSurveyUserResultItem.AnswerWeight.Value;
                                            hitblSurveyUserResultItem.Answer += item.Text + ",";
                                        }
                                        catch (Exception exception)
                                        {
                                        }

                                    }
                                }
                                hitblSurveyUserResultItem.Answer = hitblSurveyUserResultItem.Answer.TrimEnd(',');
                                sbMailText.AppendFormat("{0} <br/><hr/>", hitblSurveyUserResultItem.Answer);

                                break;
                            case SurveyAnswersType.MultipleChoiceOnlyOneAnswer:
                                answerKey = string.Format("rbl{0}", surveyQuestion.SQU_ID);
                                RadioButtonList rbl = AnswerControls[answerKey] as RadioButtonList;
                                if (rbl.SelectedIndex >= 0)
                                {
                                    try
                                    {
                                        hitblSurveyUserResultItem.AnswerWeight = +Convert.ToDouble(rbl.SelectedValue.Split('_')[0]);
                                        calculatePoints = calculatePoints + Convert.ToDouble(rbl.SelectedValue.Split('_')[0]);

                                        hitblSurveyUserResultItem.Answer += rbl.SelectedItem.Text;
                                        sbMailText.AppendFormat("{0} <br/><hr/>", rbl.SelectedItem.Text);
                                    }
                                    catch (Exception exception)
                                    {
                                    }

                                }
                                else
                                {
                                    isInputComplete = false;
                                }
                                break;
                            default:
                                break;
                        }
                        hitblSurveyUserResult.hitbl_Survey_User_ResultItem_SUIs.Add(hitblSurveyUserResultItem);
                    }
                }
            }
            if (isInputComplete)
            {
                hitblSurveyUserResult.TotalTestResult = calculatePoints;
                if (dataObjectSurvey.PunkteGruen > dataObjectSurvey.PunkteRot)
                {
                    //The more points the "greener"
                    if (calculatePoints >= 0.0 && calculatePoints <= dataObjectSurvey.PunkteRot)
                    {
                        hitblSurveyUserResult.Light = SurveySemaphore.Red.ToString();
                    }
                    else if (calculatePoints > dataObjectSurvey.PunkteRot && calculatePoints <= dataObjectSurvey.PunkteGelb)
                    {
                        hitblSurveyUserResult.Light = SurveySemaphore.Yellow.ToString();
                    }
                    else
                    {
                        hitblSurveyUserResult.Light = SurveySemaphore.Green.ToString();
                    }

                }
                else
                {
                    //The more points the "reder"
                    if (calculatePoints >= 0.0 && calculatePoints <= dataObjectSurvey.PunkteGruen)
                    {
                        hitblSurveyUserResult.Light = SurveySemaphore.Red.ToString();
                    }
                    else if (calculatePoints > dataObjectSurvey.PunkteGruen && calculatePoints <= dataObjectSurvey.PunkteGelb)
                    {
                        hitblSurveyUserResult.Light = SurveySemaphore.Yellow.ToString();
                    }
                    else
                    {
                        hitblSurveyUserResult.Light = SurveySemaphore.Green.ToString();
                    }

                }
                radMP.Visible = false;
                pager.Controls.Clear();
                litHeader.Controls.Clear();
                litFooter.Controls.Clear();
                var surveyResult = (from allResults in surveyDataClassDataContext.hitbl_Survey_TestResult_STRs.Where(x => x.OBJ_ID == dataObjectSurvey.ObjectID.Value && calculatePoints >= x.ValueFrom && calculatePoints <= x.ValueTo)
                                    orderby allResults.ValueFrom ascending
                                    select allResults).SingleOrDefault();
                if (surveyResult != null)
                {
                    litHeader.Text = surveyResult.ResultText;
                }
                else
                {
                    litHeader.Text = !dataObjectSurvey.IsContest ? string.Format(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("DefaultTestResult"), dataObjectSurvey.Title) : string.Format(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("DefaultContestResult"), dataObjectSurvey.Title);
                }
                if (dataObjectSurvey.ShowForm)
                {
                    hidResult.Value = litHeader.Text;
                    hidQA.Value = sbMailText.ToString();
                    hidSURID.Value = hitblSurveyUserResult.SUR_ID.ToString();
                    pnlFormular.Visible = true;
                    RevEMail.ValidationExpression = Constants.REGEX_EMAIL;
                    FillFormData();
                    litHeader.Text = string.Empty;
                }
                hitblSurveyUserResult.TestResultText = litHeader.Text;
                surveyDataClassDataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
            }
            else
            {
                litFooter.Text = "<div class=\"errorText\" style=\"margin-top:10px;\">Bitte beantworten Sie alle Fragen!</div>";
            }
        }

        private void FillFormData()
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();
            if (udc.IsAuthenticated)
            {
                DataObjectUser user = DataObjectUser.Load<DataObjectUser>(udc.UserID, null, true);
                if (user.Sex.IndexOf("-1") > -1)
                    Helper.Ddl_SelectItem(this.Sex, 1);
                else if (user.Sex.IndexOf("0") > -1)
                    Helper.Ddl_SelectItem(this.Sex, 0);
                else if (user.Sex.IndexOf("1") > -1)
                    Helper.Ddl_SelectItem(this.Sex, 1);
                else
                    Helper.Ddl_SelectItem(this.Sex, -1);

                this.Name.Text = user.Name;
                this.Vorname.Text = user.Vorname;
                this.AddressStreet.Text = user.AddressStreet;
                this.AddressZip.Text = user.AddressZip;
                this.AddressCity.Text = user.AddressCity;
                this.EMail.Text = Membership.GetUser(udc.UserID).Email;
                this.Phone.Text = user.Phone;
            }
        }


        protected void OnAjaxUpdate(object sender, ToolTipUpdateEventArgs e)
        {
            string[] tooltipId = e.TargetControlID.Split(new char[] { '_' });
            if (tooltipId.Length == 4)
            {
                Literal literal = new Literal
                                      {
                                          Text =
                                              AdWordHelper.GetCampaignContent(new Guid(tooltipId[0]),
                                                                              new Guid(tooltipId[1]),
                                                                              UserDataContext.GetUserDataContext(),
                                                                              tooltipId[2],
                                                                              "Popup")
                                      };
                literal.Text = Regex.Replace(literal.Text,
                                             @"(/Pages/Other/AdCampaignRedirecter.aspx\?CID=\w{8}-\w{4}-\w{4}-\w{4}-\w{12})",
                                             "$1&OID=" + tooltipId[1] + "&Word=" + tooltipId[2] + "&Type=PopupLink");
                e.UpdatePanel.ContentTemplateContainer.Controls.Add(literal);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            radMP.Visible = false;
            pager.Controls.Clear();
            litHeader.Controls.Clear();
            litFooter.Controls.Clear();
            litHeader.Text = Request.Form[hidResult.UniqueID];
            litHeader.Visible = true;
            UserDataContext udc = UserDataContext.GetUserDataContext();
            StringBuilder sbUserData = new StringBuilder();

            sbUserData.Append("<table style='width:100%'>");
            sbUserData.AppendFormat("<tr><td colspan='2'>{0}</td></tr>", Request.Form[hidQA.UniqueID]);
            sbUserData.AppendFormat("<tr><td>{0}: </td><td>{1} </td></tr>", "Anrede", Sex.SelectedItem.Text);
            sbUserData.AppendFormat("<tr><td>{0}: </td><td>{1} </td></tr>", "Name", Name.Text);
            sbUserData.AppendFormat("<tr><td>{0}: </td><td>{1} </td></tr>", "Vorname", Vorname.Text);
            sbUserData.AppendFormat("<tr><td>{0}: </td><td>{1} </td></tr>", "Firma", Firma.Text);
            sbUserData.AppendFormat("<tr><td>{0}: </td><td>{1} </td></tr>", "Strasse", AddressStreet.Text);
            sbUserData.AppendFormat("<tr><td>{0}: </td><td>{1} </td></tr>", "PLZ", AddressZip.Text);
            sbUserData.AppendFormat("<tr><td>{0}: </td><td>{1} </td></tr>", "Ort", AddressCity.Text);
            sbUserData.AppendFormat("<tr><td>{0}: </td><td>{1} </td></tr>", "E-Mail-Adresse", EMail.Text);
            sbUserData.AppendFormat("<tr><td>{0}: </td><td>{1} </td></tr>", "Telefon", Phone.Text);
            sbUserData.AppendFormat("<tr><td>{0}: </td><td>{1} </td></tr>", "Bemerkungen", Comment.Text);
            sbUserData.Append("</table>");
            hitbl_Survey_User_Result_SUR hitblSurveyUserResult = (from allUserResults in surveyDataClassDataContext.hitbl_Survey_User_Result_SURs.Where(x => x.SUR_ID == Request.Form[hidSURID.UniqueID].ToGuid())
                                                                  select allUserResults).SingleOrDefault();
            hitblSurveyUserResult.TestResultText = litHeader.Text;
            hitblSurveyUserResult.Name = Name.Text;
            hitblSurveyUserResult.Vorname = Vorname.Text;
            hitblSurveyUserResult.Email = EMail.Text;
            hitblSurveyUserResult.Address = string.Format("{0}<br/>{1}<br/>{2},{3}", Firma.Text, AddressStreet.Text, AddressZip.Text, AddressCity.Text);
            surveyDataClassDataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

            if (!string.IsNullOrEmpty(dataObjectSurvey.MailTo))
            {
                string subject = !dataObjectSurvey.IsContest ? string.Format(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("FormSurveySubject"), dataObjectSurvey.Title) : string.Format(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("FormContestSubject"), dataObjectSurvey.Title);
                SendEmail(new List<string>() { dataObjectSurvey.MailTo }, subject, sbUserData.ToString(), EMail.Text);
            }

            pnlFormular.Visible = false;
        }

        private static bool SendEmail(List<string> strMailUsers, string subject, string messageTxt, string replyMail)
        {
            string strMailBody = GuiLanguage.GetGuiLanguage("Templates").GetString("EmailRecommendation");

            SmtpSection smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            MailMessage objMail = new MailMessage
                                      {
                                          From = new MailAddress(smtpSec.From),
                                          Subject = subject,
                                          Body = messageTxt,
                                          IsBodyHtml = true
                                      };
            foreach (string strMailTo in strMailUsers)
            {
                if (!string.IsNullOrEmpty(strMailTo))
                {
                    objMail.To.Add(new MailAddress(strMailTo));
                }
            }
            if (!string.IsNullOrEmpty(replyMail))
            {
                objMail.ReplyTo = new MailAddress(replyMail);
            }
            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Send(objMail);

            return true;
        }

    }
}