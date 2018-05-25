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
  public class ServiceLocationConverter : TypeConverter
  {
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      ICollection keys = Program.GetProperties().GetServiceUrls().Keys;

      return new StandardValuesCollection(keys);
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
