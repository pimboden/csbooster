using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4screen.CSB.DataAccess.Business
{
    public class UserExport
    {
        public static UserExportList GetUserExportList()
        {
            return Data.UserExport.GetUserExportList();
        }
    }
}
