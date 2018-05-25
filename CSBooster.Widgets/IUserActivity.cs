using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IUserActivity
    {
        UserActivityParameters UserActivityParameters { get; set; }
        bool HasContent { get; set; }
        string OutputTemplate { get; set; }
    }
}