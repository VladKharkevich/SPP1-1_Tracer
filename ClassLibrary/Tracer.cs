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
        private RootResult fRootResult;

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
            foreach  (ThreadResult thResult in fRootResult.Threads)
            {
                if (thResult.Id == threadId)
                    return thResult;
            }
            ThreadResult threadResult = new ThreadResult(threadId, 0);
            fRootResult.Threads.Add(threadResult);
            return threadResult;
        }

        private TraceResult CreateTraceResult(ThreadResult threadResult)
        {
            string methodName = GetMethodName();
            string className = GetClassName();
            TraceResult traceResult = new TraceResult(className, methodName, (Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds);
            StackTrace stackTrace = new StackTrace();
            List<string> stackMethods = new List<string>();
            int i = 2;
            while (true)
            {
                MethodBase methodic = stackTrace.GetFrame(i).GetMethod();
                if (methodic.Name == "Main" || methodic.Name == "ThreadStart_Context")
                    break;
                i++;
                stackMethods.Add(methodic.Name);
            }
            stackMethods.Reverse();
            bool isFirst = true;
            TraceResult traceIter = traceResult;

            foreach (string methodNameFuck in stackMethods)
            {
                if (isFirst)
                {
                    isFirst = false;
                    bool isExist = false;
                    foreach (TraceResult traceTemp in threadResult.DependenceMethods)
                    {
                        if (traceTemp.MethodName == methodNameFuck)
                        {
                            traceIter = traceTemp;
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        threadResult.DependenceMethods.Add(traceResult);
                        break;
                    }
                }
                else
                {
                    bool isExist = false;
                    foreach (TraceResult traceTemp in traceIter.DependenceMethods)
                    {
                        if (traceTemp.MethodName == methodNameFuck)
                        {
                            traceIter = traceTemp;
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        traceIter.DependenceMethods.Add(traceResult);
                        break;
                    }
                }
            }
            return traceResult;
        }

        public Tracer()
        {
            fRootResult = new RootResult(0);
            ThreadResult threadResult = new ThreadResult(System.Threading.Thread.CurrentThread.ManagedThreadId, 0);
            fRootResult.Threads.Add(threadResult);
        }

        public RootResult GetTraceResult()
        {
            return fRootResult;
        }

        public void StartTrace()
        {
            ThreadResult threadResult = GetOrCreateThreadResult();
            CreateTraceResult(threadResult);
        } 

        public void StopTrace()
        {
            ThreadResult threadResult = GetOrCreateThreadResult();
            string methodName = GetMethodName();
            StackTrace stackTrace = new StackTrace();
            List<string> stackMethods = new List<string>();
            int i = 1;
            while (true)
            {
                MethodBase methodic = stackTrace.GetFrame(i).GetMethod();
                Console.WriteLine(methodic.Name);
                if (methodic.Name == "Main" || methodic.Name == "ThreadStart_Context")
                    break;
                i++;
                stackMethods.Add(methodic.Name);
            }
            stackMethods.Reverse();
            bool isFirst = true;
            TraceResult traceIter = new TraceResult("", "", 0);
            foreach (string methodNameFuck in stackMethods)
            {
                if (isFirst)
                {
                    isFirst = false;
                    foreach (TraceResult traceTemp in threadResult.DependenceMethods)
                    {
                        if (traceTemp.MethodName == methodNameFuck)
                        {
                            traceIter = traceTemp;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (TraceResult traceTemp in traceIter.DependenceMethods)
                    {
                        if (traceTemp.MethodName == methodNameFuck)
                        {
                            traceIter = traceTemp;
                            break;
                        }
                    }
                }
            }
            traceIter.Time = (Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds - traceIter.Time;
        }
    }
}
