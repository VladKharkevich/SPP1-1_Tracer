﻿using System;
using System.Collections.Generic;
using YAXLib;

namespace ClassLibrary
{
    public class TraceResult
    {   
        public TraceResult(string className, string methodName, long time)
        {
            this.Time = time;
            this.ClassName = className;
            this.MethodName = methodName;
            this.DependenceMethods = new List<TraceResult>();
        }
        [YAXAttributeForClass()]
        [YAXSerializeAs("class")]
        public string ClassName { get; internal set; }

        [YAXAttributeForClass()]
        [YAXSerializeAs("time")]
        public long Time { get; internal set; }

        [YAXSerializeAs("methodName")]
        [YAXAttributeForClass()]
        public string MethodName { get; internal set; }

        [YAXSerializeAs("methods")]
        [YAXAttributeForClass()]
        public List<TraceResult> DependenceMethods { get; internal set; }
    }
}
