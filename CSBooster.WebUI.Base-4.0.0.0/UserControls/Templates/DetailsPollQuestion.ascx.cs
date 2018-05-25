// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using Telerik.Charting;
using Telerik.Charting.Styles;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsPollQuestion : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        protected DataObject dataObject;
        private DataObjectPollQuestion pollQuestion;
        private UserDataContext udc;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base");
        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pollQuestion = (DataObjectPollQuestion)dataObject;
            udc = UserDataContext.GetUserDataContext();

            // Disable tooltip managers by default -> tooltip managers without targets don't work
            this.RTTM.Visible = false;
            this.RTTMIMG.Visible = false;
            if (pollQuestion != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(pollQuestion.ObjectID.Value))
                {
                    this.RTTM.TargetControls.Add(tooltipId, true);
                    this.RTTM.Visible = true;
                }
            }

            this.DESCLIT.Text = pollQuestion.DescriptionLinked;
            this.LitVon.Text = pollQuestion.StartDate.ToShortDateString();
            this.LitBis.Text = pollQuestion.EndDate.ToShortDateString();

            RenderPoll(null);
        }

        private void RenderPoll(DataObjectPollQuestion.AnswerResult? result)
        {
            pollQuestion = DataObjectPollQuestion.Load<DataObjectPollQuestion>(pollQuestion.ObjectID, null, true);

            bool showChart = false;
            bool showAnswer = false;
            bool hasVoted = pollQuestion.HasVoted;
            bool showButton = false;

            if (pollQuestion.EndDate >= DateTime.Now.GetEndOfDay() && pollQuestion.StartDate <= DateTime.Now.GetStartOfDay())
            {
                if (udc.IsAuthenticated || pollQuestion.AnonymousAllowed)
                {
                    if (!hasVoted)
                        showButton = true;
                }
            }

            if (pollQuestion.EndDate >= DateTime.Now.GetStartOfDay())
            {
                if (udc.IsAuthenticated || pollQuestion.AnonymousAllowed)
                {
                    if (!hasVoted)
                        showAnswer = true;
                }
            }
            if (!showAnswer)
                showButton = false;

            if (pollQuestion.StartDate <= DateTime.Now.GetStartOfDay())
            {
                if (pollQuestion.AnswerTotal > 0 && pollQuestion.ShowResult != DataObjectPollQuestion.QuestionShowResult.ShowChartNever)
                {
                    if (pollQuestion.ShowResult == DataObjectPollQuestion.QuestionShowResult.ShowChartAlways)
                    {
                        showChart = true;
                    }
                    else if (hasVoted)
                    {
                        showChart = true;
                    }
                    else if (!udc.IsAuthenticated && !pollQuestion.AnonymousAllowed)
                    {
                        showChart = true;
                    }
                }
            }
            if (pollQuestion.EndDate < DateTime.Now)
            {
                showChart = true;
            }

            LitResult.Visible = false;
            if (result.HasValue)
            {
                if (pollQuestion.CountRightAnswer() > 0)
                {
                    if (result == DataObjectPollQuestion.AnswerResult.Right && pollQuestion.TextRight.Length > 0)
                        this.LitResult.Text = pollQuestion.TextRight;
                    else if (result == DataObjectPollQuestion.AnswerResult.Partially && pollQuestion.TextPartially.Length > 0)
                        LitResult.Text = pollQuestion.TextPartially;
                    else if (result == DataObjectPollQuestion.AnswerResult.False && pollQuestion.TextFalse.Length > 0)
                        LitResult.Text = pollQuestion.TextFalse;
                    else
                        LitResult.Text = language.GetString("MessagePollThank");
                }
                else
                {
                    LitResult.Text = language.GetString("MessagePollThank");
                }
                LitResult.Visible = true;
            }

            PnlAnser.Visible = showAnswer;
            LbtAnswer.Visible = showButton;

            if (showAnswer)
            {
                if (pollQuestion.PollType == DataObjectPollQuestion.QuestionPollType.SingleAnswer && this.RblAnswer.Items.Count == 0)
                {
                    foreach (DataObjectPollQuestion.PollAnswer item in pollQuestion.Answers)
                    {
                        this.RblAnswer.Items.Add(new ListItem(item.Answer, item.Position.ToString()));
                    }
                }
                else if (pollQuestion.PollType == DataObjectPollQuestion.QuestionPollType.MultiAnswer && this.CblAnswer.Items.Count == 0)
                {
                    foreach (DataObjectPollQuestion.PollAnswer item in pollQuestion.Answers)
                    {
                        this.CblAnswer.Items.Add(new ListItem(item.Answer, item.Position.ToString()));
                    }
                }

                RblAnswer.Visible = (RblAnswer.Items.Count > 1);
                CblAnswer.Visible = (CblAnswer.Items.Count > 1);
            }

            PnlResult.Visible = showChart;
            if (showChart)
            {
                Chart.PlotArea.Appearance.FillStyle.FillType = FillType.Solid;
                Chart.PlotArea.Appearance.FillStyle.MainColor = Color.FromArgb(0, 0, 0, 0);
                Chart.PlotArea.Appearance.Border.Width = 0f;
                Chart.PlotArea.Appearance.Dimensions.Margins.Bottom = 0;
                Chart.PlotArea.Appearance.Dimensions.Margins.Left = 0;
                Chart.PlotArea.Appearance.Dimensions.Margins.Right = 0;
                Chart.PlotArea.Appearance.Dimensions.Margins.Top = 0;
                Chart.PlotArea.XAxis.Visible = ChartAxisVisibility.False;
                Chart.PlotArea.YAxis.Visible = ChartAxisVisibility.False;

                int width = 0;
                if (Settings.ContainsKey("Width") && Settings["Width"] != null)
                {
                    int.TryParse(Settings["Width"].ToString(), out width);
                }
                if (width > 0)
                {
                    this.Chart.Width = new System.Web.UI.WebControls.Unit(width);
                }
                if (pollQuestion.PollLayout == DataObjectPollQuestion.QuestionPollLayout.Bar)
                {
                    if (width > 0)
                        this.Chart.Height = new System.Web.UI.WebControls.Unit(Convert.ToInt32(30 * pollQuestion.Answers.Count));
                    RenderChartBar();
                }
                else if (pollQuestion.PollLayout == DataObjectPollQuestion.QuestionPollLayout.Pie)
                {
                    if (width > 0)
                        this.Chart.Height = new System.Web.UI.WebControls.Unit(Convert.ToInt32(width * 0.9));
                    RenderChartPie();
                }
            }

        }

        private void RenderChartPie()
        {
            this.Chart.Chart.Series.Clear();
            ChartSeries series = new ChartSeries("ANSWER", ChartSeriesType.Pie);
            Chart.Series.Add(series);
            int maxValue = GetMaxValue();
            int totalValue = GetTotalValue();

            pollQuestion.Answers.Sort(new DataObjectPollQuestion.TotalSorterPollAnswer());
            foreach (DataObjectPollQuestion.PollAnswer answer in pollQuestion.Answers)
            {
                this.Chart.PlotArea.XAxis.AddItem(answer.Answer);
                if (pollQuestion.ShowAnswerCount == DataObjectPollQuestion.QuestionShowAnswerCount.Percent)
                    series.AddItem(answer.Total, string.Format("{0} ({1:0}%)", answer.Answer, 100.0 / totalValue * answer.Total));
                else if (pollQuestion.ShowAnswerCount == DataObjectPollQuestion.QuestionShowAnswerCount.Number)
                    series.AddItem(answer.Total, string.Format("{0} ({1:0})", answer.Answer, answer.Total));
                else
                    series.AddItem(answer.Total, answer.Answer);
            }

            Chart.PlotArea.XAxis.LayoutMode = ChartAxisLayoutMode.Between;
            Chart.PlotArea.YAxis.AutoScale = false;
            Chart.PlotArea.YAxis.MinValue = 0;
            Chart.PlotArea.YAxis.MaxValue = maxValue;

            if (maxValue <= 10)
                Chart.PlotArea.XAxis.Step = 1;
            else if (maxValue <= 100)
                Chart.PlotArea.XAxis.Step = 10;
            else if (maxValue <= 1000)
                Chart.PlotArea.XAxis.Step = 100;
            else
                Chart.PlotArea.XAxis.Step = 200;
        }

        private void RenderChartBar()
        {
            this.Chart.Chart.Series.Clear();
            ChartSeries series = new ChartSeries("ANSWER", ChartSeriesType.Bar);
            series.Appearance.Border.Width = 2;
            Chart.Series.Add(series);
            int maxValue = GetMaxValue();
            int totalValue = GetTotalValue();

            pollQuestion.Answers.Sort(new DataObjectPollQuestion.TotalSorterPollAnswer());
            foreach (DataObjectPollQuestion.PollAnswer answer in pollQuestion.Answers)
            {
                this.Chart.PlotArea.XAxis.AddItem(answer.Answer);
                if (pollQuestion.ShowAnswerCount == DataObjectPollQuestion.QuestionShowAnswerCount.Percent)
                    series.AddItem(answer.Total, string.Format("{0} ({1:0}%)", answer.Answer, 100.0 / totalValue * answer.Total));
                else if (pollQuestion.ShowAnswerCount == DataObjectPollQuestion.QuestionShowAnswerCount.Number)
                    series.AddItem(answer.Total, string.Format("{0} ({1:0})", answer.Answer, answer.Total));
                else
                    series.AddItem(answer.Total, answer.Answer);
            }

            maxValue++;
            Chart.PlotArea.XAxis.LayoutMode = ChartAxisLayoutMode.Between;
            Chart.PlotArea.YAxis.AutoScale = false;
            Chart.PlotArea.YAxis.MinValue = 0;
            Chart.PlotArea.YAxis.MaxValue = maxValue;


            if (maxValue <= 10)
                Chart.PlotArea.XAxis.Step = 1;
            else if (maxValue <= 100)
                Chart.PlotArea.XAxis.Step = 10;
            else if (maxValue <= 1000)
                Chart.PlotArea.XAxis.Step = 100;
            else
                Chart.PlotArea.XAxis.Step = 200;
        }

        private int GetMaxValue()
        {
            int maxValue = 0;
            foreach (DataObjectPollQuestion.PollAnswer answer in pollQuestion.Answers)
            {
                if (answer.Total > maxValue)
                    maxValue = answer.Total;
            }
            return maxValue;
        }

        private int GetTotalValue()
        {
            int totalValue = 0;
            foreach (DataObjectPollQuestion.PollAnswer answer in pollQuestion.Answers)
            {
                totalValue += answer.Total;
            }
            return totalValue;
        }


        protected void OnAjaxUpdate(object sender, Telerik.Web.UI.ToolTipUpdateEventArgs e)
        {
            string[] tooltipId = e.TargetControlID.Split(new char[] { '_' });
            if (tooltipId.Length == 4)
            {
                Literal literal = new Literal();
                literal.Text = _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignContent(new Guid(tooltipId[0]), new Guid(tooltipId[1]), UserDataContext.GetUserDataContext(), tooltipId[2], "Popup");
                literal.Text = System.Text.RegularExpressions.Regex.Replace(literal.Text, @"(/Pages/Other/AdCampaignRedirecter.aspx\?CID=\w{8}-\w{4}-\w{4}-\w{4}-\w{12})", "$1&OID=" + tooltipId[1] + "&Word=" + tooltipId[2] + "&Type=PopupLink");
                e.UpdatePanel.ContentTemplateContainer.Controls.Add(literal);
            }
        }

        protected void LbtAnswer_Click(object sender, EventArgs e)
        {
            List<int> answers = new List<int>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    if (key.IndexOf("RblAnswer") > 1)
                    {
                        answers.Add(Convert.ToInt32(Request.Form[key]));
                    }
                    else if (key.IndexOf("CblAnswer") > 1)
                    {
                        int last = key.LastIndexOf("$");
                        answers.Add(Convert.ToInt32(key.Substring(last + 1)));
                    }
                }
            }
            if (answers.Count > 0)
            {
                pollQuestion = DataObjectPollQuestion.Load<DataObjectPollQuestion>(pollQuestion.ObjectID, null, true);

                RenderPoll(DataObjectPollQuestion.AddPollAnswer(UserDataContext.GetUserDataContext(), pollQuestion, answers));
            }

        }
    }
}