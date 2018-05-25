//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - DialogEngine
//
//  Created:    #1.0.0.0                22.08.2007 15:21:43 / aw
//                                    - Dialog conditions fixed
//******************************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;

namespace _4screen.CSB.DataAccess.Business
{
    public class Dialog : IComparable<Dialog>
    {
        private Guid dialogId;
        private string pageName;
        private DateTime activeFromDate;
        private MethodInfo conditionMethodInfo;
        private Dictionary<string, string> parameters;
        private int conditionTrueValue;
        private string title;
        private string content;

        public Dialog(Guid dialogId, string pageName, DateTime activeFromDate, MethodInfo conditionMethodInfo, Dictionary<string, string> parameters, int conditionTrueValue, string title, string content)
        {
            this.dialogId = dialogId;
            this.pageName = pageName;
            this.activeFromDate = activeFromDate;
            this.conditionMethodInfo = conditionMethodInfo;
            this.parameters = parameters;
            this.conditionTrueValue = conditionTrueValue;
            this.title = title;
            this.content = content;
        }

        public Guid DialogId
        {
            get { return dialogId; }
        }

        public Dictionary<string, string> Parameters
        {
            get { return parameters; }
        }

        public DateTime ActiveFromDate
        {
            get { return activeFromDate; }
        }

        public MethodInfo ConditionMethodInfo
        {
            get { return conditionMethodInfo; }
        }

        public string ConditionMethod
        {
            get { return conditionMethodInfo.Name; }
        }

        public string PageName
        {
            get { return pageName; }
        }

        public int ConditionTrueValue
        {
            get { return conditionTrueValue; }
        }

        public string Title
        {
            get { return title; }
        }

        public string Content
        {
            get { return content; }
        }

        public int CompareTo(Dialog x)
        {
            int comparison = x.ConditionMethod.CompareTo(ConditionMethod);
            if (comparison == 0)
                comparison = ConditionTrueValue.CompareTo(x.ConditionTrueValue);
            return comparison;
        }
    }
}