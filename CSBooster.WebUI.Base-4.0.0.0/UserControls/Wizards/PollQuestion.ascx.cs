// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class PollQuestion : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        private DataObjectPollQuestion pollQuestion;

        protected void Page_Load(object sender, EventArgs e)
        {
            pollQuestion = DataObject.Load<DataObjectPollQuestion>(ObjectID, null, true);

            if (pollQuestion.State == ObjectState.Added)
            {
                pollQuestion.ObjectID = ObjectID;
                pollQuestion.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                pollQuestion.Description = language.GetString("LabelQuestion");
                pollQuestion.CommunityID = CommunityID;
                pollQuestion.ShowState = ObjectShowState.Draft;
                pollQuestion.StartDate = DateTime.Now.GetStartOfDay();
                pollQuestion.EndDate = pollQuestion.StartDate.AddMonths(1).GetEndOfDay();
                pollQuestion.Insert(UserDataContext.GetUserDataContext());
                pollQuestion.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                pollQuestion.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                pollQuestion.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                pollQuestion.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                pollQuestion.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        pollQuestion.Geo_Lat = geoLat;
                        pollQuestion.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                pollQuestion.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                pollQuestion.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                pollQuestion.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                pollQuestion.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            if (!IsPostBack)
                FillAnswers();

            FillEditForm();
        }

        private void FillEditForm()
        {
            this.TxtQTitle.Text = pollQuestion.Title;
            this.TxtQuestion.Text = pollQuestion.Description;
            if (pollQuestion.StartDate > this.RDPEndDate.MaxDate)
                this.RDPStartDate.SelectedDate = this.RDPStartDate.MaxDate;
            else
                this.RDPStartDate.SelectedDate = pollQuestion.StartDate;

            if (pollQuestion.EndDate > this.RDPEndDate.MaxDate)
                this.RDPEndDate.SelectedDate = this.RDPEndDate.MaxDate;
            else
                this.RDPEndDate.SelectedDate = pollQuestion.EndDate;

            if (DdlType.Items.Count == 0)
                FillDropDown(DdlType, typeof(DataObjectPollQuestion.QuestionPollType), "QuestionPollType");
            Helper.Ddl_SelectItem(DdlType, (int)pollQuestion.PollType);

            if (DdlLayout.Items.Count == 0)
                FillDropDown(DdlLayout, typeof(DataObjectPollQuestion.QuestionPollLayout), "QuestionPollLayout");
            Helper.Ddl_SelectItem(DdlLayout, (int)pollQuestion.PollLayout);

            if (DdlAnswerCount.Items.Count == 0)
                FillDropDown(DdlAnswerCount, typeof(DataObjectPollQuestion.QuestionShowAnswerCount), "QuestionPollShowAnswerCount");
            Helper.Ddl_SelectItem(DdlAnswerCount, (int)pollQuestion.ShowAnswerCount);

            this.CbxAnonymous.Checked = pollQuestion.AnonymousAllowed;

            if (DdlShowResult.Items.Count == 0)
                FillDropDown(DdlShowResult, typeof(DataObjectPollQuestion.QuestionShowResult), "QuestionPollShowResult");
            Helper.Ddl_SelectItem(DdlShowResult, (int)pollQuestion.ShowResult);

            this.TxtRight.Text = pollQuestion.TextRight;
            this.TxtFalse.Text = pollQuestion.TextFalse;
            this.TxtPartially.Text = pollQuestion.TextPartially;  

            this.HFTagWords.Value = pollQuestion.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)pollQuestion.Status).ToString();
            this.HFShowState.Value = ((int)pollQuestion.ShowState).ToString();
            this.HFCopyright.Value = pollQuestion.Copyright.ToString();
            if (pollQuestion.Geo_Lat != double.MinValue && pollQuestion.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = pollQuestion.Geo_Lat.ToString();
                this.HFGeoLong.Value = pollQuestion.Geo_Long.ToString();
            }
            this.HFZip.Value = pollQuestion.Zip;
            this.HFCity.Value = pollQuestion.City;
            this.HFStreet.Value = pollQuestion.Street;
            this.HFCountry.Value = pollQuestion.CountryCode;

            if (pollQuestion.HasAnswers)
            {
                this.TxtQuestion.Enabled = false;
                this.RDPStartDate.Enabled = false; 
                this.DdlType.Enabled = false; 
                this.LstAnswers.Enabled = false;
                this.LbtAdd.Enabled = false;
                this.LbtDel.Enabled = false;
                this.TxtAnswer.Enabled = false;
                this.LitMsg.Text = language.GetString("MessagePollReadOnly");
            }
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                pollQuestion.Title = Common.Extensions.StripHTMLTags(this.TxtQTitle.Text).CropString(100);
                if (!pollQuestion.HasAnswers)
                {
                    pollQuestion.Description = Common.Extensions.StripHTMLTags(this.TxtQuestion.Text).CropString(500);
                    if (this.RDPStartDate.SelectedDate.HasValue)
                        pollQuestion.StartDate = this.RDPStartDate.SelectedDate.Value;

                    pollQuestion.PollType = (DataObjectPollQuestion.QuestionPollType)Convert.ToInt32((string) this.DdlType.SelectedItem.Value);
                }
                if (this.RDPEndDate.SelectedDate.HasValue)
                    pollQuestion.EndDate = this.RDPEndDate.SelectedDate.Value;

                pollQuestion.PollLayout = (DataObjectPollQuestion.QuestionPollLayout)Convert.ToInt32((string) this.DdlLayout.SelectedItem.Value);
                pollQuestion.ShowAnswerCount = (DataObjectPollQuestion.QuestionShowAnswerCount)Convert.ToInt32((string) this.DdlAnswerCount.SelectedItem.Value);   
                pollQuestion.AnonymousAllowed = this.CbxAnonymous.Checked;
                pollQuestion.ShowResult = (DataObjectPollQuestion.QuestionShowResult)Convert.ToInt32((string) this.DdlShowResult.SelectedItem.Value);
                pollQuestion.TextRight = this.TxtRight.Text;
                pollQuestion.TextFalse = this.TxtFalse.Text;
                pollQuestion.TextPartially = this.TxtPartially.Text;

                pollQuestion.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                pollQuestion.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                pollQuestion.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                pollQuestion.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    pollQuestion.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    pollQuestion.Geo_Long = geoLong;
                pollQuestion.Zip = this.HFZip.Value;
                pollQuestion.City = this.HFCity.Value;
                pollQuestion.Street = this.HFStreet.Value;
                pollQuestion.CountryCode = this.HFCountry.Value;

                pollQuestion.Update(UserDataContext.GetUserDataContext());

                return true;
            }
            catch (Exception ex)
            {
                this.LitMsg.Text = "Fehler beim Speichern: " + ex.Message;
                return false;
            }
        }

        private void FillAnswers()
        {
            this.LstAnswers.Items.Clear();
            foreach (DataObjectPollQuestion.PollAnswer item in pollQuestion.Answers)
            {
                if (item.IsRight) 
                    this.LstAnswers.Items.Add(new ListItem(string.Format("{0} +", item.Answer), item.Position.ToString()));
                else
                    this.LstAnswers.Items.Add(new ListItem(item.Answer, item.Position.ToString()));      
            }
            LbtDel.Enabled = (LstAnswers.Items.Count > 0);
        }

        protected void LbtDel_Click(object sender, EventArgs e)
        {
            if (Request.Form[this.LstAnswers.UniqueID] != null)
            {
                int position = Common.Extensions.ToInt32(Request.Form[this.LstAnswers.UniqueID], -1);
                if (position > -1)
                {
                    pollQuestion.RemoveAnswer(position);
                    pollQuestion.Update(UserDataContext.GetUserDataContext());
                    FillAnswers();
                }
            }
        }

        protected void LbtAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtAnswer.Text))
            {
                pollQuestion.AddAnswer(Common.Extensions.StripHTMLTags(TxtAnswer.Text).CropString(100), CbxRight.Checked);
                pollQuestion.Update(UserDataContext.GetUserDataContext());  
                FillAnswers();
                TxtAnswer.Text = string.Empty; 
            }
        }

        private void FillDropDown(DropDownList ddl, Type enuType, string prefix)
        {

            foreach (object item in Enum.GetValues(enuType))
            {
                string name = Enum.GetName(enuType, item).ToString();
                string key = string.Format("{0}_{1}", prefix, name);
                string id = Convert.ToInt32(Enum.Parse(enuType, item.ToString())).ToString();

                string text = language.GetString(key);
                if (string.IsNullOrEmpty(text))
                    text = name;

                ddl.Items.Add(new ListItem(text, id));

            }
        }
    }
}