// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System.ServiceProcess;

namespace _4screen.CSB.WindowServices
{
    static class Program
    {
        static void Main()
        {
            ServiceBase[] servicesToRun = new ServiceBase[] 
                                              { 
                                                  new VideoEncodingService() 
                                              };
            ServiceBase.Run(servicesToRun);
        }
    }
}
