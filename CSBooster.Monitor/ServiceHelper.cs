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
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Reflection;
using System.Configuration;

namespace _4screen.CSB.Monitor
{
  class ServiceHelper
  {
    private static Dictionary<string, Service> services = new Dictionary<string, Service>();
    private static Dictionary<string, string> serviceNameMap = new Dictionary<string, string>();
    private static Dictionary<string, string> reverseServiceNameMap = new Dictionary<string, string>();

    static ServiceHelper()
    {
      // Load [service name <-> display name] mappings form the application config
      MethodInfo[] methodInfos = typeof(Service).GetMethods();

      Hashtable hashtable = (Hashtable)ConfigurationManager.GetSection("webMethodNameMappings");
      foreach (MethodInfo methodInfo in methodInfos)
      {
        if (hashtable[methodInfo.Name] != null)
        {
          serviceNameMap.Add(methodInfo.Name, (string)hashtable[methodInfo.Name]);
          reverseServiceNameMap.Add((string)hashtable[methodInfo.Name], methodInfo.Name);
        }
      }

      // Override certificate check -> allow all certificates
      ServicePointManager.ServerCertificateValidationCallback += delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                                                                 {
                                                                   return true;
                                                                 };
    }

    public static Service GetService(string url)
    {
      if (!services.ContainsKey(url))
      {
        Service service = new Service();
        service.Timeout = 30000;
        service.Url = url;
        service.AuthenticationHeaderValue = CSBoosterMonitor.GetAuthHeader();
        services.Add(url, service);
        ServiceForm serviceForm = CSBoosterMonitor.GetServiceForm();
        serviceForm.AppendMessage("Lade Service [" + service.Url + "] ...");
      }
      return services[url];
    }

    public static List<string> GetMethodNamesByReturnType(Type returnType)
    {
      List<string> list = new List<string>();

      // Find a method which matches the return type and doesn't take an IAsyncResult as parameter
      MethodInfo[] methodInfos = typeof(Service).GetMethods();
      foreach (MethodInfo methodInfo in methodInfos)
      {
        if (methodInfo.ReturnType == returnType)
        {
          ParameterInfo[] parameterInfos = methodInfo.GetParameters();
          bool methodValid = true;
          foreach (ParameterInfo parameterInfo in parameterInfos)
          {
            if (parameterInfo.ParameterType == typeof(System.IAsyncResult))
            {
              methodValid = false;
            }
          }
          if (methodValid)
          {
            list.Add(ServiceHelper.GetMappedServiceName(methodInfo.Name));
          }
        }
      }

      return list;
    }

    public static MethodInfo GetMethodInfo(string methodName, string prefix, string postfix, Type[] parameterTypes)
    {
      try
      {
        return typeof(Service).GetMethod(prefix + ServiceHelper.GetReversedMappedServiceName(methodName) + postfix, parameterTypes);
      }
      catch
      {
        return null;
      }
    }

    private static string GetMappedServiceName(string name)
    {
      if (ServiceHelper.serviceNameMap.ContainsKey(name))
        return ServiceHelper.serviceNameMap[name];
      else
        return name;
    }

    private static string GetReversedMappedServiceName(string name)
    {
      if (ServiceHelper.reverseServiceNameMap.ContainsKey(name))
        return ServiceHelper.reverseServiceNameMap[name];
      else
        return name;
    }
  }
}
