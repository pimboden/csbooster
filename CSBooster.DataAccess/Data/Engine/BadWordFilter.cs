//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - FilterEngine
//
//  Created:    #1.0.0.0                10.08.2007 11:02:36 / aw
//  Updated:   
//******************************************************************************

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _4screen.CSB.DataAccess.Data
{
    internal class BadWordFilter : IWordFilter
    {
        private string word;
        private string wordPattern;
        private bool isExactMatch;
        private BadWordFilterActions action;
        private Dictionary<string, string> parameters;
        private string replacementPattern;
        private Regex regex;

        internal BadWordFilter(string word, bool isExactMatch, BadWordFilterActions action, Dictionary<string, string> parameters)
        {
            this.word = word;
            this.isExactMatch = isExactMatch;
            this.action = action;
            this.parameters = parameters;

            string replacement = "XXX";
            if (this.parameters.ContainsKey("replacement"))
                replacement = this.parameters["replacement"];

            if (isExactMatch)
            {
                wordPattern = @"(^|\W)" + word + @"(\W|$)";
                replacementPattern = "$1" + replacement + "$2";
                regex = new Regex(wordPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            else
            {
                wordPattern = word;
                replacementPattern = replacement;
                regex = new Regex(wordPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
        }

        public string Process(string value, Type type, FilterObjectTypes filterObjectType, Guid objectId, Guid userId)
        {
            string processedValue = value;

            if (!string.IsNullOrEmpty(processedValue))
            {
                if ((action & BadWordFilterActions.Inform) == BadWordFilterActions.Inform)
                {
                    if (regex.IsMatch(value))
                    {
                        BadWordHelper.InformAdmin(action, value, word, isExactMatch, type, filterObjectType, objectId, userId);
                    }
                }
                if ((action & BadWordFilterActions.Censor) == BadWordFilterActions.Censor)
                {
                    if (regex.IsMatch(value))
                    {
                        processedValue = regex.Replace(value, replacementPattern);
                    }
                }
                if ((action & BadWordFilterActions.Lock) == BadWordFilterActions.Lock)
                {
                    if (regex.IsMatch(value))
                    {
                        BadWordHelper.LockUser(userId);
                    }
                }
            }

            return processedValue;
        }
    }
}