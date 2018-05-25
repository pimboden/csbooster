using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4screen.CSB.DataAccess.Business
{
    public class UserExportList
    {
        public List<string> Fields { get; set; }
        public List<List<string>> Users { get; set; }

        public UserExportList()
        {
            Fields = new List<string>();
            Users = new List<List<string>>();
        }
    }
}
