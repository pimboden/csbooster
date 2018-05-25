// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;

namespace _4screen.CSB.DataAccess.Business
{
    [Serializable]
    public class ViewLogParams
    {
        public Guid? ObjectID { get; set; }

        public override string ToString()
        {
            if (ObjectID.HasValue)
                return string.Format("O{0}", ObjectID.Value);
            else
                return string.Empty;
        }
    }
}
