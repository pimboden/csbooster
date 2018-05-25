using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4screen.CSB.Widget
{
     public interface IRoleVisibilityAndFixation
    {
         Guid InstanceID { get; set; }
     
         void Save();
    }
}
