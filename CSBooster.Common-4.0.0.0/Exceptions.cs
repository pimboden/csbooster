// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;

namespace _4screen.CSB.Common
{
    public sealed class SiemeException : Exception
    {
        public SiemeException(string className, string methodName, string message)
            : base(string.Format("Class={0}, Method={1}, Message={2}", className, methodName, message))
        {
            this.Source = string.Format("Class={0}, Method={1}", className, methodName);
        }
        public SiemeException(string className, string methodName, string message, Exception innerException)
            : base(string.Format("Class={0}, Method={1}, Message={2}", className, methodName, message), innerException)
        {
            this.Source = string.Format("Class={0}, Method={1}", className, methodName);
        }
    }

    public sealed class SiemeArgumentException : Exception
    {
        public SiemeArgumentException(string className, string methodName, string argument, string message)
            : base(string.Format("Class={0}, Method={1}, Argument={2}, Message={3}", className, methodName, argument, message))
        {
            this.Source = string.Format("Class={0}, Method={1}", className, methodName);
        }
        public SiemeArgumentException(string className, string methodName, string argument, string message, Exception innerException)
            : base(string.Format("Class={0}, Method={1}, Argument={2}, Message={3}", className, methodName, argument, message), innerException)
        {
            this.Source = string.Format("Class={0}, Method={1}", className, methodName);
        }
    }

    public sealed class SiemeSecurityException : Exception
    {
        public SiemeSecurityException(string className, string methodName, AccessMode accessMode, string message)
            : base(string.Format("Class={0}, Method={1}, AccessMode={2}, Message={3}", className, methodName, accessMode, message))
        {
            this.Source = string.Format("Class={0}, Method={1}", className, methodName);
        }
        public SiemeSecurityException(string className, string methodName, AccessMode accessMode, string message, Exception innerException)
            : base(string.Format("Class={0}, Method={1}, AccessMode={2}, Message={3}", className, methodName, accessMode, message), innerException)
        {
            this.Source = string.Format("Class={0}, Method={1}", className, methodName);
        }
    }
}
