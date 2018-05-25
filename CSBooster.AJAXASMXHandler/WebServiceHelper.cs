// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace AJAXASMXHandler
{
    internal class WebServiceDef
    {
        public Type WSType;
        public Dictionary<string, WebMethodDef> Methods;

        public WebServiceDef(Type wsType)
        {
            WSType = wsType;

            EnsureMethods(wsType);
        }

        /// <summary>
        /// Scans the web service type for Web methods and precaches web method definition
        /// </summary>
        /// <param name="wsType"></param>
        private void EnsureMethods(Type wsType)
        {
            lock (this)
            {
                List<Type> typeList = new List<Type>();

                // Run through the inheritence tree and get all base types
                Type current = wsType;
                typeList.Add(current);
                while (current.BaseType != null)
                {
                    current = current.BaseType;
                    typeList.Add(current);
                }

                // Find all Web methods in the web service and its base class inheritence tree
                Dictionary<string, WebMethodDef> methods = new Dictionary<string, WebMethodDef>(StringComparer.OrdinalIgnoreCase);

                for (int i = typeList.Count - 1; i >= 0; --i)
                {
                    MethodInfo[] methodInfos = typeList[i].GetMethods(WebMethodDef.BINDING_FLAGS);
                    foreach (MethodInfo method in methodInfos)
                    {
                        AddMethod(wsType, methods, method);
                    }
                }

                Methods = methods;
            }
        }

        /// <summary>
        /// Caches a web method definition 
        /// </summary>
        /// <param name="wsType"></param>
        /// <param name="methods"></param>
        /// <param name="method"></param>
        private void AddMethod(Type wsType, Dictionary<string, WebMethodDef> methods, MethodInfo method)
        {
            object[] wmAttribs = method.GetCustomAttributes(typeof (WebMethodAttribute), false);

            if (wmAttribs.Length == 0)
                return;

            ScriptMethodAttribute sm = null;
            object[] responseAttribs = method.GetCustomAttributes(typeof (ScriptMethodAttribute), false);
            if (responseAttribs.Length > 0)
            {
                sm = (ScriptMethodAttribute) responseAttribs[0];
            }


            TransactionalMethodAttribute tm = null;
            object[] tmAttribs = method.GetCustomAttributes(typeof (TransactionalMethodAttribute), false);
            if (tmAttribs.Length > 0)
            {
                tm = (TransactionalMethodAttribute) tmAttribs[0];
            }

            WebMethodDef wmd = new WebMethodDef(this, method, (WebMethodAttribute) wmAttribs[0], sm, tm);
            methods[wmd.MethodName] = wmd;
        }
    }

    /// <summary>
    /// Caches a web method definition along with its BeginXXX and EndXXX pairs
    /// </summary>
    internal class WebMethodDef
    {
        public string MethodName;
        public List<ParameterInfo> InputParameters;
        public List<ParameterInfo> InputParametersWithAsyc;
        public bool HasAsyncMethods;
        public WebMethodDef BeginMethod;
        public WebMethodDef EndMethod;
        public MethodInfo MethodType;
        public WebMethodAttribute WebMethodAtt;
        public ScriptMethodAttribute ScriptMethodAtt;
        public TransactionalMethodAttribute TransactionAtt;
        public ResponseFormat ResponseFormat;
        public bool IsGetAllowed;

        public const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.DeclaredOnly;

        public WebMethodDef(WebServiceDef wsDef, MethodInfo method, WebMethodAttribute wmAttribute, ScriptMethodAttribute smAttribute, TransactionalMethodAttribute tmAttribute)
        {
            MethodType = method;
            WebMethodAtt = wmAttribute;
            ScriptMethodAtt = smAttribute;
            TransactionAtt = tmAttribute;

            if (wmAttribute != null && !string.IsNullOrEmpty(wmAttribute.MessageName))
                MethodName = wmAttribute.MessageName;
            else
                MethodName = method.Name;

            // HTTP GET method is allowed only when there's a [ScriptMethod] attribute and UseHttpGet is true
            IsGetAllowed = (ScriptMethodAtt != null && ScriptMethodAtt.UseHttpGet);

            ResponseFormat = (ScriptMethodAtt != null ? ScriptMethodAtt.ResponseFormat : ResponseFormat.Json);

            MethodInfo beginMethod = wsDef.WSType.GetMethod("Begin" + method.Name, BINDING_FLAGS);
            if (null != beginMethod)
            {
                // The BeginXXX method must have the [ScriptMethod] attribute
                object[] scriptMethodAttributes = beginMethod.GetCustomAttributes(typeof (ScriptMethodAttribute), false);
                if (scriptMethodAttributes.Length > 0)
                {
                    // Asynchronous methods found for the function
                    HasAsyncMethods = true;

                    BeginMethod = new WebMethodDef(wsDef, beginMethod, null, null, null);

                    MethodInfo endMethod = wsDef.WSType.GetMethod("End" + method.Name, BINDING_FLAGS);
                    EndMethod = new WebMethodDef(wsDef, endMethod, null, null, null);

                    // get all parameters of begin web method and then leave last two parameters in the input parameters list because
                    // last two parameters are for AsyncCallback and Async State
                    ParameterInfo[] allParameters = beginMethod.GetParameters();
                    ParameterInfo[] inputParameters = new ParameterInfo[allParameters.Length - 2];
                    Array.Copy(allParameters, inputParameters, allParameters.Length - 2);

                    BeginMethod.InputParameters = new List<ParameterInfo>(inputParameters);
                    BeginMethod.InputParametersWithAsyc = new List<ParameterInfo>(allParameters);
                }
            }
            else
            {
                InputParameters = new List<ParameterInfo>(method.GetParameters());
            }
        }
    }

    internal class WebServiceHelper
    {
        internal static string GetCacheKey(string vpath)
        {
            return "ASMXHandler.WebServiceHelper:" + vpath;
        }

        internal static WebServiceDef GetWebServiceType(HttpContext context, string virtualPath)
        {
            virtualPath = VirtualPathUtility.ToAbsolute(virtualPath.Replace(".soap", ".asmx"));

            string cacheKey = GetCacheKey(virtualPath);
            WebServiceDef wsType = context.Cache[cacheKey] as WebServiceDef;

            if (wsType == null)
            {
                if (HostingEnvironment.VirtualPathProvider.FileExists(virtualPath))
                {
                    Type compiledType = null;
                    try
                    {
                        compiledType = BuildManager.GetCompiledType(virtualPath);


                        if (compiledType == null)
                        {
                            object page = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof (System.Web.UI.Page));
                            compiledType = page.GetType();
                        }
                    }
                    catch (SecurityException)
                    {
                    }

                    if (compiledType != null)
                    {
                        wsType = new WebServiceDef(compiledType);
                        BuildDependencySet deps = BuildManager.GetCachedBuildDependencySet(context, virtualPath);
                        IEnumerable virtualPaths = deps.VirtualPaths;
                        if (virtualPaths != null)
                        {
                            List<string> paths = new List<string>();
                            foreach (string path in virtualPaths)
                            {
                                paths.Add(Path.Combine(context.Request.PhysicalApplicationPath, VirtualPathUtility.GetFileName(path)));
                            }
                            context.Cache.Insert(cacheKey, wsType, new CacheDependency(paths.ToArray()));
                        }
                        else
                        {
                            context.Cache.Insert(cacheKey, wsType);
                        }
                    }
                }
            }

            if (wsType == null)
            {
                throw new InvalidOperationException("Webservice does not exist");
            }

            return wsType;
        }

        internal class WebServiceError
        {
            public string Message;
            public string StackTrace;
            public string ExceptionType;

            public WebServiceError(string msg, string stack, string type)
            {
                Message = msg;
                StackTrace = stack;
                ExceptionType = type;
            }
        }

        internal static void WriteExceptionJsonString(HttpContext context, Exception ex, JavaScriptSerializer serializer)
        {
            context.AddError(ex);
            context.Response.ClearHeaders();
            context.Response.ClearContent();
            context.Response.Clear();
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.StatusDescription = HttpWorkerRequest.GetStatusDescription((int) HttpStatusCode.InternalServerError);
            context.Response.ContentType = "application/json";
            context.Response.AddHeader("jsonerror", "true");
            using (StreamWriter writer = new StreamWriter(context.Response.OutputStream, new UTF8Encoding(false)))
            {
                if (ex is TargetInvocationException)
                {
                    ex = ex.InnerException;
                }


                if (context.IsCustomErrorEnabled)
                {
                    writer.Write(serializer.Serialize(new WebServiceError("Error occured while processing request", String.Empty, String.Empty)));
                }
                else
                {
                    writer.Write(serializer.Serialize(new WebServiceError(ex.Message, ex.StackTrace, ex.GetType().FullName)));
                }
                writer.Flush();
            }
        }
    }
}