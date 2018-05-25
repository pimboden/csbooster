// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{

    [XmlRoot(ElementName = "PollQuestion")]
    public class DataObjectPollQuestion : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {

        public enum QuestionPollType
        {
            SingleAnswer = 1,
            MultiAnswer = 2
        }

        public enum QuestionShowAnswerCount
        {
            None = 0,
            Number = 1,
            Percent = 2
        }

        public enum QuestionPollLayout
        {
            Pie = 1,
            Bar = 2
        }

        public enum QuestionShowResult
        {
            ShowChartAfterVoting = 0,
            ShowChartAlways = 1,
            ShowChartNever = 2
        }

        public enum AnswerResult
        {
            AlreadyVoted = 0,
            Right = 1,
            False = 2,
            Partially = 3
        }

        List<PollAnswer> list = null;
        internal XmlDocument answerXml;
        public QuestionPollType PollType { get; set; }
        public QuestionShowAnswerCount ShowAnswerCount { get; set; }
        public QuestionShowResult ShowResult { get; set; }
        public QuestionPollLayout PollLayout { get; set; }
        public bool AnonymousAllowed { get; set; }
        public string TextRight { get; set; }
        public string TextFalse { get; set; }
        public string TextPartially { get; set; }

        public List<PollAnswer> Answers
        {
            get
            {
                if (list == null)
                {
                    list = new List<PollAnswer>();

                    foreach (XmlElement question in answerXml.SelectNodes("Root/Answer"))
                    {
                        PollAnswer item = new PollAnswer();
                        item.Anonnymous = Convert.ToInt32(question.GetAttribute("Anonnymous"));
                        item.Registerd = Convert.ToInt32(question.GetAttribute("Registerd"));
                        item.Position = Convert.ToInt32(question.GetAttribute("Position"));
                        item.IsRight = (question.GetAttribute("IsRight").ToLower() == "true");
                        item.Answer = question.InnerText;
                        list.Add(item);
                    }
                }
                return list;
            }
            set
            {
                if (!HasAnswers)
                {
                    UpdateAnswers(value);
                }
            }
        }

        public void AddAnswer(string text, bool isRight)
        {
            if (!string.IsNullOrEmpty(text))
            {
                PollAnswer item = new PollAnswer();
                Answers.Add(item);

                item.Anonnymous = 0;
                item.Registerd = 0;
                item.Position = list.Count;
                item.Answer = text;
                item.IsRight = isRight;
                UpdateAnswers(list);
            }
        }

        public void RemoveAnswer(int position)
        {
            if (Answers.Count > position)
            {
                Answers.RemoveAt(position);
                UpdateAnswers(list);
            }
        }

        internal void UpdateAnswers(List<PollAnswer> theList)
        {
            answerXml.DocumentElement.RemoveAll();

            int sort = 0;
            foreach (PollAnswer kvp in theList)
            {
                if (!string.IsNullOrEmpty(kvp.Answer))
                {
                    XmlElement item = answerXml.DocumentElement.AppendChild(answerXml.CreateElement("Answer")) as XmlElement;
                    item.SetAttribute("Position", sort.ToString());
                    item.SetAttribute("Anonnymous", kvp.Anonnymous.ToString());
                    item.SetAttribute("Registerd", kvp.Registerd.ToString());
                    item.SetAttribute("IsRight", kvp.IsRight.ToString());
                    item.InnerText = kvp.Answer;
                    sort++;
                }
            }
            list = null;
            objectState = ObjectState.Changed;
        }

        public bool HasAnswers
        {
            get { return (this.AnswerTotal > 0); }
        }

        public int AnswerAnonnymous
        {
            get { return CountAnswer("Anonnymous"); }
        }

        public int AnswerRegisterd
        {
            get { return CountAnswer("Registerd"); }
        }

        private int CountAnswer(string answerType)
        {
            int count = 0;
            foreach (PollAnswer kvp in this.Answers)
            {
                if (answerType == "Anonnymous")
                    count += kvp.Anonnymous;
                else if (answerType == "Registerd")
                    count += kvp.Registerd;
            }
            return count;
        }

        public int CountRightAnswer()
        {
            int count = 0;
            foreach (PollAnswer kvp in this.Answers)
            {
                if (kvp.IsRight)
                    count++;
            }
            return count;
        }

        public int AnswerTotal
        {
            get { return AnswerAnonnymous + AnswerRegisterd; }
        }

        public bool IsReadyForVoting
        {
            get { return (StartDate <= DateTime.Now && EndDate >= DateTime.Now && Answers.Count > 1); }
        }

        public bool HasVoted
        {
            get { return Data.DataObjectPollAnswer.HasVoted(ObjectID.Value, ObjectType, Helper.GetObjectType("PollAnswer").NumericId, UserDataContext.GetUserDataContext()); }
        }

        public DataObjectPollQuestion()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectPollQuestion(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("PollQuestion").NumericId;
            AnonymousAllowed = false;
            PollType = QuestionPollType.SingleAnswer;
            ShowAnswerCount = QuestionShowAnswerCount.Percent;
            PollLayout = QuestionPollLayout.Bar;
            ShowResult = QuestionShowResult.ShowChartAfterVoting;
            answerXml = new XmlDocument();
            XmlHelper.CreateRoot(answerXml, "Root");
        }

        public static AnswerResult AddPollAnswer(UserDataContext udc, DataObjectPollQuestion pollQuestion, List<int> questionPosition)
        {
            return AddPollAnswer(udc, pollQuestion, questionPosition, null, null);
        }

        public static AnswerResult AddPollAnswer(UserDataContext udc, DataObjectPollQuestion pollQuestion, List<int> questionPosition, string comment, string nickName)
        {
            AnswerResult retVal = AnswerResult.AlreadyVoted;

            if (pollQuestion != null && pollQuestion.IsReadyForVoting)
            {
                if (udc.IsAuthenticated || pollQuestion.AnonymousAllowed)
                {
                    if (pollQuestion.HasVoted)
                        return retVal;

                    int rightAnswers = pollQuestion.CountRightAnswer();
                    int rightAnswer = 0;

                    int max = questionPosition.Count;
                    if (pollQuestion.PollType == QuestionPollType.SingleAnswer)
                        max = 1;

                    for (int x = 0; x < max; x++)
                    {
                        DataObjectPollAnswer pollAnswer = new DataObjectPollAnswer(udc);
                        pollAnswer.CommunityID = pollQuestion.CommunityID;
                        pollAnswer.Title = pollQuestion.Answers[questionPosition[x]].Answer.CropString(100);
                        if (!string.IsNullOrEmpty(nickName))
                            pollAnswer.nickname = nickName.CropString(256);

                        pollAnswer.Position = questionPosition[x];
                        pollAnswer.Answer = 1;

                        pollAnswer.Insert(udc, pollQuestion.ObjectID.Value, pollQuestion.ObjectType);

                        if (udc.IsAuthenticated)
                            pollQuestion.Answers[questionPosition[x]].Registerd += pollAnswer.Answer;
                        else
                            pollQuestion.Answers[questionPosition[x]].Anonnymous += pollAnswer.Answer;

                        if (pollQuestion.Answers[questionPosition[x]].IsRight)
                            rightAnswer++;
                    }

                    pollQuestion.UpdateAnswers(pollQuestion.Answers);
                    pollQuestion.UpdateBackground();

                    if (rightAnswer == rightAnswers && rightAnswer == max)
                        retVal = AnswerResult.Right;
                    else if (rightAnswer == rightAnswers && rightAnswer != max)
                        retVal = AnswerResult.Partially;
                    else if (rightAnswer < rightAnswers && rightAnswer > 0)
                        retVal = AnswerResult.Partially;
                    else
                        retVal = AnswerResult.False;
                }
            }
            return retVal;
        }

        public new void Validate(AccessMode accessMode)
        {
            base.Validate(accessMode);
        }

        public new void Delete(UserDataContext udc, bool showStateOnly)
        {
            DataObjectList<DataObject> relatedObjects = DataObjects.Load<DataObject>(new QuickParameters()
            {
                RelationParams = new RelationParams() { ParentObjectID = this.ObjectID },
                Udc = udc,
                DisablePaging = true,
                Amount = 999999
            });
            foreach (var relatedObject in relatedObjects)
            {
                relatedObject.Delete(udc, showStateOnly);
            }
            base.Delete(udc, showStateOnly);
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectPollQuestion.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPollQuestion.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectPollQuestion.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectPollQuestion.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPollQuestion.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPollQuestion.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPollQuestion.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPollQuestion.GetOrderBySQL(qParas, parameters);
        }
        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectPollQuestion));
            XmlDocument xmlDocument = new XmlDocument();
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, this);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            xmlDocument.Load(stream);
            stream.Close();

            SerializationType = SerializationType.Transfer;
            return xmlDocument;
        }

        public override string GetOutput(string outputType, string templatesUrl, XsltArgumentList argumentList)
        {
            string baseUrlXSLT = string.Empty;
            if (PartnerID.HasValue)
            {
                Partner partner = Partner.Get(PartnerID.Value, null);
                if (partner != null && !string.IsNullOrEmpty(partner.BaseUrlXSLT))
                    baseUrlXSLT = partner.BaseUrlXSLT;
            }
            return Helper.TransformDataObject(ObjectType, GetXml(), UrlXSLT, outputType, baseUrlXSLT, templatesUrl, argumentList);
        }

        #region IXmlSerializable

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public new void ReadXml(System.Xml.XmlReader reader)
        {
            while (reader.Read())
            {
                base.ReadXml(reader);

                if (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "PollType":
                            this.PollType = (QuestionPollType)reader.ReadString().ToInt32(1);
                            break;
                        case "AnonymousAllowed":
                            AnonymousAllowed = bool.Parse(reader.ReadString());
                            break;
                        case "ShowResult":
                            ShowResult = (QuestionShowResult)reader.ReadString().ToInt32(1);
                            break;
                        case "ShowAnswerCount":
                            ShowAnswerCount = (QuestionShowAnswerCount)reader.ReadString().ToInt32(1);
                            break;
                        case "PollLayout":
                            PollLayout = (QuestionPollLayout)reader.ReadString().ToInt32(1);
                            break;
                        case "TextRight":
                            TextRight = reader.ReadString();
                            break;
                        case "TextFalse":
                            TextFalse = reader.ReadString();
                            break;
                        case "TextPartially":
                            TextPartially = reader.ReadString();
                            break;
                        case "Answers":
                            PollAnswer item = null;
                            System.Xml.XmlReader subReader = reader.ReadSubtree();
                            while (subReader.Read())
                            {
                                if (subReader.NodeType == System.Xml.XmlNodeType.Element)
                                {
                                    if (subReader.Name == "Answer")
                                    {
                                        if (item != null)
                                            list.Add(item);
                                        item = new PollAnswer();
                                        item.Answer = subReader.ReadString();
                                    }
                                }
                                else if (subReader.NodeType == System.Xml.XmlNodeType.Attribute)
                                {
                                    switch (subReader.Name)
                                    {
                                        case "Position":
                                            item.Position = Convert.ToInt32(subReader.ReadString());
                                            break;
                                        case "Anonnymous":
                                            item.Anonnymous = Convert.ToInt32(subReader.ReadString());
                                            break;
                                        case "Registerd":
                                            item.Registerd = Convert.ToInt32(subReader.ReadString());
                                            break;
                                        case "IsRight":
                                            item.IsRight = Convert.ToBoolean(subReader.ReadString());
                                            break;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);

            writer.WriteElementString("PollType", PollType.ToString("D"));
            writer.WriteElementString("ShowAnswerCount", ShowAnswerCount.ToString("D"));
            writer.WriteElementString("ShowResult", ShowResult.ToString("D"));
            writer.WriteElementString("PollLayout", PollLayout.ToString("D"));
            writer.WriteElementString("AnonymousAllowed", AnonymousAllowed.ToString());
            writer.WriteElementString("HasAnswers", HasAnswers.ToString());
            writer.WriteElementString("AnswerAnonnymous", AnswerAnonnymous.ToString());
            writer.WriteElementString("AnswerRegisterd", AnswerRegisterd.ToString());
            writer.WriteElementString("AnswerTotal", AnswerTotal.ToString());
            writer.WriteElementString("TextRight", TextRight);
            writer.WriteElementString("TextFalse", TextFalse);
            writer.WriteElementString("TextPartially", TextPartially);

            writer.WriteStartElement("Answers");
            foreach (PollAnswer kvp in Answers)
            {
                writer.WriteStartElement("Answer");
                writer.WriteAttributeString("Position", kvp.Position.ToString());
                writer.WriteAttributeString("Anonnymous", kvp.Anonnymous.ToString());
                writer.WriteAttributeString("Registerd", kvp.Registerd.ToString());
                writer.WriteAttributeString("IsRight", kvp.IsRight.ToString());
                writer.WriteString(kvp.Answer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        #endregion

        public class PollAnswer
        {
            public PollAnswer()
            {
                Position = 0;
                Anonnymous = 0;
                Registerd = 0;
                IsRight = false;
            }
            public string Answer { get; set; }
            public int Position { get; set; }
            public int Anonnymous { get; internal set; }
            public int Registerd { get; internal set; }
            public bool IsRight { get; set; }
            public int Total
            {
                get { return Anonnymous + Registerd; }
            }
        }

        public class TotalSorterPollAnswer : IComparer<PollAnswer>
        {
            private bool blnDesc = false;

            public TotalSorterPollAnswer(bool desc)
            {
                blnDesc = desc;
            }

            public TotalSorterPollAnswer()
            {
                blnDesc = false;
            }

            public int Compare(PollAnswer x, PollAnswer y)
            {
                if (x.Total.CompareTo(y.Total) == 0)
                {
                    if (!blnDesc)
                        return y.Total.CompareTo(x.Total);
                    else
                        return x.Total.CompareTo(y.Total);
                }
                else
                {
                    if (!blnDesc)
                        return x.Total.CompareTo(y.Total);
                    else
                        return y.Total.CompareTo(x.Total);
                }
            }
        }

    }

}