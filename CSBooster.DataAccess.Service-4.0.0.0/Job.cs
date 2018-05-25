// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Reflection;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Service
{
    class Job
    {
        private int intStartType = 0;
        private TimeSpan tspStartTime = new DateTime(2007, 1, 1, 0, 0, 1).TimeOfDay;
        private int intWaitMinutes = 60;
        private DateTime datLastRunning = DateTime.Now;
        private XmlNodeList xmlList;
        private string strCachingPath;
        private bool blnIsRunning = false;
        private bool blnCancel = false;
        private DateTime datExpirationDate = DateTime.Now.AddDays(2);
        private object dynObj = null;
        private MethodInfo cancelMethod = null;

        public System.Diagnostics.EventLog EventLog { get; set; }

        public DateTime ExpirationDate
        {
            get { return datExpirationDate; }
            set { datExpirationDate = value; }
        }

        public bool IsRunning
        {
            get { return blnIsRunning; }
        }

        public Job(XmlNode xmlJob, string cachingPath)
        {
            xmlList = xmlJob.SelectNodes("Execute");
            strCachingPath = cachingPath;
        }

        public int StartType
        {
            get { return intStartType; }
            set { intStartType = value; }
        }

        public TimeSpan StartTime
        {
            get { return tspStartTime; }
            set { tspStartTime = value; }
        }

        public int WaitMinutes
        {
            get { return intWaitMinutes; }
            set { intWaitMinutes = value; }
        }

        public DateTime LastRunning
        {
            get { return datLastRunning; }
            set { datLastRunning = value; }
        }

        public void Cancel()
        {
            blnCancel = true;
            if (dynObj != null && cancelMethod != null)
            {
                try
                {
                    cancelMethod.Invoke(dynObj, null);
                }
                catch
                {
                    // do nothing
                }
            }
        }

        public string Start()
        {
            StringBuilder strMsg = new StringBuilder();
            try
            {
                blnIsRunning = true;
                blnCancel = false;
                foreach (XmlElement xmlItem in xmlList)
                {
                    if (blnCancel)
                        break;

                    if (xmlItem.HasAttribute("activ") && xmlItem.GetAttribute("activ").ToLower() == "false")
                        continue;

                    int index = 0;
                    object[] parameters = new object[xmlItem.ChildNodes.Count];
                    foreach (XmlNode xmlPara in xmlItem.ChildNodes)
                    {
                        parameters[index] = xmlPara.InnerText.Trim();
                        index++;
                    }

                    string strAsm = string.Concat(string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"\"), xmlItem.GetAttribute("assembly"));
                    string strClass = xmlItem.GetAttribute("className");
                    string strMethod = xmlItem.GetAttribute("methodName");

                    DateTime datStart = DateTime.Now;
                    object obj = InvokeMethod(strAsm, strClass, strMethod, parameters);
                    if (xmlItem.GetAttribute("log").ToLower() == "true")
                    {
                        DateTime datRun = new DateTime(DateTime.Now.Ticks - datStart.Ticks);

                        string strRet = string.Empty;
                        if (obj != null)
                            strRet = obj.ToString();

                        if (strMsg.Length > 0)
                            strMsg.AppendLine();

                        strMsg.AppendLine(string.Format("Assembly: {0}", xmlItem.GetAttribute("assembly")));
                        strMsg.AppendLine(string.Format("Class: {0}", strClass));
                        strMsg.Append(string.Format("Method: {0}", strMethod));
                        strMsg.Append("(");
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            strMsg.AppendFormat("'{0}'", parameters[i].ToString());

                            if (i < parameters.Length - 1)
                                strMsg.Append(", ");
                        }
                        strMsg.Append(")");
                        strMsg.AppendLine();
                        strMsg.AppendLine(string.Format("Seconds: {0}", (datRun.Hour * 3600) + (datRun.Minute * 60) + datRun.Second));
                        if (strRet.Length > 0)
                            strMsg.AppendLine(string.Format("Return: {0}", strRet));

                    }
                }
            }
            finally
            {
                blnIsRunning = false;
            }
            return strMsg.ToString();
        }

        private object InvokeMethod(string asmPath, string typeName, string methodName, object[] parameters)
        {
            try
            {
                // Assembly laden
                Assembly asm = Assembly.LoadFrom(asmPath);
                // Den Typ ermitteln
                Type dynClassType = asm.GetType(typeName, true, false);
                // Erstellen einer Instanz
                dynObj = Activator.CreateInstance(dynClassType);

                if (dynObj != null)
                {
                    if (dynObj is IEventLog)
                        ((IEventLog)dynObj).EventLog = EventLog;

                    // Prüfen ob das CachingPath-Property existiert
                    PropertyInfo propInfo;
                    propInfo = dynClassType.GetProperty("CachingPath");
                    if (propInfo != null)
                    {
                        propInfo.SetValue(dynObj, strCachingPath, null);
                        propInfo = null;
                    }

                    propInfo = dynClassType.GetProperty("ExpirationDate");
                    if (propInfo != null)
                    {
                        propInfo.SetValue(dynObj, ExpirationDate, null);
                        propInfo = null;
                    }

                    // Prüfen ob die Methode 'Cancel' existiert
                    cancelMethod = dynClassType.GetMethod("Cancel");


                    // Prüfen ob die Methode existiert
                    MethodInfo invokedMethod = dynClassType.GetMethod(methodName);
                    if (invokedMethod != null)
                    {
                        int index = 0;
                        // Die Parameterliste aufbauen
                        foreach (object parameter in parameters)
                        {
                            Type paramType = invokedMethod.GetParameters()[index].ParameterType;
                            if (paramType.IsEnum)
                                parameters[index] = Enum.Parse(paramType, parameter.ToString());
                            else
                                parameters[index] = Convert.ChangeType(parameter, paramType);

                            index++;
                        }

                        // Die methode aufrufen
                        object retObj = invokedMethod.Invoke(dynObj, parameters);
                        return retObj;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                dynObj = null;
                cancelMethod = null;
            }

            return null;
        }
    }
}
