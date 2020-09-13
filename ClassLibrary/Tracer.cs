using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{

    public class Tracer : ITracer
    {
        private TraceResult fTraceResult;
        private Int64 fStartTime;

        private string GetMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(2).GetMethod();
            return method.Name;
        }

        private string GetClassName()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(2).GetMethod();
            string methodName = method.Name;
            return method.ReflectedType.Name;
        }

        public Tracer()
        {
            string methodName = GetMethodName();
            string className = GetClassName();
            fTraceResult = new TraceResult(className, methodName, 0);
        }

        public TraceResult GetTraceResult()
        {
            return fTraceResult;
        }

        public void StartTrace()
        {
            fStartTime = (Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }

        public void StopTrace()
        {
             fTraceResult.Time = (Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds - fStartTime;
        }
    }
}
