using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace _4screen.CSB.WebServices
{
   static class Program
   {
      static void Main()
      {
         ServiceBase[] ServicesToRun;
         ServicesToRun = new ServiceBase[] 
			{ 
				new ServiceHost() 
			};
         ServiceBase.Run(ServicesToRun);
      }
   }
}
