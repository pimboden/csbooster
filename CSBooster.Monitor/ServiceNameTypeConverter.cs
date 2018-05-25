//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.Collections;
using System.ComponentModel;

namespace _4screen.CSB.Monitor
{
  public class ServiceNameTypeConverter : TypeConverter
  {
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      if (context.Instance is IServiceSwitchable)
        return new StandardValuesCollection(ServiceHelper.GetMethodNamesByReturnType(((IServiceSwitchable)context.Instance).GetServiceReturnType()));
      else
        return new StandardValuesCollection(new string[] { "no valid context" });
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return true;
    }
  }
}
