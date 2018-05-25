//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using System.Collections.Generic;
using _4screen.CSB.Common;
using System;

namespace _4screen.CSB.DataAccess.Business
{
    public static class CountryNames
    {
        public static List<CountryName> Load()
        {
            return Data.CountryNames.Load(CultureHandler.GetCurrentSpecificLanguagCode());
        }

        public static List<CountryName> Load(string langCode)
        {
            return Data.CountryNames.Load(langCode);
        }
    }

    public class CountryName
    {
        internal CountryName()
        {
        }

        public string Name { get; internal set; }
        public string LangCode { get; internal set; }
        public string CountryCode { get; internal set; }

    }
}