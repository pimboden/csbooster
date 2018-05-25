using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4screen.CSB.Widget
{
    public interface ISmallOutputUser
    {
        string UserName { get; set; }
        string UserPictureURL { get; set; }
        string PrimaryColor { get; set; }
        string SecondaryColor { get; set; }
        string UserDetailURL { get; set; }
    }
}
