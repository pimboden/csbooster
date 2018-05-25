// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Collections.Specialized;
using System.Text;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class QuickParametersDataObjectSurvey : QuickParameters
    {
        #region Properties

        public bool? IsContest { get; set; }

        #endregion

        #region Constructors

        #endregion

        public QuickParametersDataObjectSurvey()
        {
            ObjectType = Helper.GetObjectTypeNumericID("Survey");
        }


        public override void FromNameValueCollection(NameValueCollection collection)
        {
            base.FromNameValueCollection(collection);
            if (!string.IsNullOrEmpty(collection["IsContest"]))
            {
                bool isContest;
                if (bool.TryParse(collection["IsContest"], out isContest))
                {
                    IsContest = isContest;
                }
                else
                {
                    IsContest = false;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());

            sb.Append("Survey");

            sb.AppendFormat("-MT{0}", IsContest);

            return sb.ToString();
        }
    }
}