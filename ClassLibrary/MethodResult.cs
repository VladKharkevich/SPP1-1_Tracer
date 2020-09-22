using System;
using System.Collections.Generic;
using YAXLib;

namespace ClassLibrary
{
    public class MethodResult
    {   
        public MethodResult(string className, string methodName, long time)
        {
            this.Time = time;
            this.ClassName = className;
            this.MethodName = methodName;
            this.DependenceMethods = new List<MethodResult>();
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

        [YAXSerializeAs("method")]
        public List<MethodResult> DependenceMethods { get; internal set; }
    }
}
