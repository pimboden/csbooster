using System;
using System.Collections.Generic;

namespace _4screen.CSB.DataAccess.Business
{
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
