using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Tracer : ITracer
    { 
        private TraceResult fTraceResult;

        private long GetUnixTimeInMilliseconds()
        {
            return (Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }

        private List<string> GetStackTraceOfMethodNames(int startCheck)
        {
            StackTrace stackTrace = new StackTrace();
            List<string> stackMethods = new List<string>();
            int i = startCheck;
            while (true)
            {
                MethodBase methodic = stackTrace.GetFrame(i).GetMethod();
                if (methodic.Name == "Main" || methodic.Name == "ThreadStart_Context" || methodic.Name == "InvokeMethod")
                    break;
                i++;
                stackMethods.Add(methodic.Name);
            }
            stackMethods.Reverse();
            return stackMethods;
        }

        private string GetMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(3).GetMethod();
            return method.Name;
        }

        private string GetClassName()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(3).GetMethod();
            string methodName = method.Name;
            return method.ReflectedType.Name;
        }

        private ThreadResult GetOrCreateThreadResult()
        {
            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            foreach  (ThreadResult thResult in fTraceResult.Threads)
            {
                if (thResult.Id == threadId)
                    return thResult;
            }
            ThreadResult threadResult = new ThreadResult(threadId, 0);
            fTraceResult.Threads.Add(threadResult);
            return threadResult;
        }

        private MethodResult CreateTraceResult(ThreadResult threadResult)
        {
            string methodName = GetMethodName();
            string className = GetClassName();
            MethodResult methodResult = new MethodResult(className, methodName, GetUnixTimeInMilliseconds());
            List<string> stackMethods = GetStackTraceOfMethodNames(3);
            bool isFirst = true;
            MethodResult traceIter = methodResult;

            foreach (string methodNameTemp in stackMethods)
            {
                if (isFirst)
                {
                    isFirst = false;
                    bool isExist = false;
                    foreach (MethodResult traceTemp in threadResult.DependenceMethods)
                    {
                        if (traceTemp.MethodName == methodNameTemp)
                        {
                            traceIter = traceTemp;
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        threadResult.DependenceMethods.Add(methodResult);
                        break;
                    }
                }
                else
                {
                    bool isExist = false;
                    foreach (MethodResult traceTemp in traceIter.DependenceMethods)
                    {
                        if (traceTemp.MethodName == methodNameTemp)
                        {
                            traceIter = traceTemp;
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        traceIter.DependenceMethods.Add(methodResult);
                        break;
                    }
                }
            }
            return methodResult;
        }

        public Tracer()
        {
            fTraceResult = new TraceResult();
            ThreadResult threadResult = new ThreadResult(System.Threading.Thread.CurrentThread.ManagedThreadId, 0);
            fTraceResult.Threads.Add(threadResult);
        }

        public TraceResult GetTraceResult()
        {
            foreach (ThreadResult threadResult in fTraceResult.Threads)
            {
                long counter_time = 0;
                foreach (MethodResult traceResult in threadResult.DependenceMethods)
                {
                    counter_time += traceResult.Time;
                }
                threadResult.Time = counter_time;
            }
            return fTraceResult;
        }

        public void StartTrace()
        {
            ThreadResult threadResult;
            lock (fTraceResult)
            {
                threadResult = GetOrCreateThreadResult();
            }
            CreateTraceResult(threadResult);
        } 

        public void StopTrace()
        {
            ThreadResult threadResult = GetOrCreateThreadResult();
            string methodName = GetMethodName();
            List<string> stackMethods = GetStackTraceOfMethodNames(2);
            bool isFirst = true;
            MethodResult traceIter = new MethodResult("", "", 0);
            foreach (string methodNameTemp in stackMethods)
            {
                if (isFirst)
                {
                    isFirst = false;
                    foreach (MethodResult traceTemp in threadResult.DependenceMethods)
                    {
                        if (traceTemp.MethodName == methodNameTemp)
                        {
                            traceIter = traceTemp;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (MethodResult traceTemp in traceIter.DependenceMethods)
                    {
                        if (traceTemp.MethodName == methodNameTemp)
                        {
                            traceIter = traceTemp;
                            break;
                        }
                    }
                }
            }
            traceIter.Time = GetUnixTimeInMilliseconds() - traceIter.Time;
        }
    }
}
