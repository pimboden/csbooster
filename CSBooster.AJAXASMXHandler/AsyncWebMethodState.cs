// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar
using System;
using System.Web;

namespace AJAXASMXHandler
{
    public class AsyncWebMethodState : IDisposable
    {
        public HttpContext Context;

        internal string MethodName;
        internal IDisposable Target;
        internal WebMethodDef MethodDef;
        internal WebServiceDef ServiceDef;
        internal object ExtraData;

        public AsyncWebMethodState(object s) : this((AsyncWebMethodState) s)
        {
        }

        public AsyncWebMethodState(AsyncWebMethodState s) : this(s.MethodName, s.Target, s.ServiceDef, s.MethodDef, s.Context, s.ExtraData)
        {
        }

        internal AsyncWebMethodState(string methodName, IDisposable target, WebServiceDef wsDef, WebMethodDef wmDef, HttpContext context, object extraData)
        {
            MethodName = methodName;
            Target = target;
            ServiceDef = wsDef;
            MethodDef = wmDef;
            Context = context;
            ExtraData = extraData;
        }

        public void Dispose()
        {
            MethodName = null;
            Target = null;
            ServiceDef = null;
            MethodDef = null;
            Context = null;
            ExtraData = null;
        }
    }
}