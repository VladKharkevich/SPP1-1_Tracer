using System;
using YAXLib;

namespace ClassLibrary
{
    public struct TraceResult
    {   
        public TraceResult(string className, string methodName, long time)
        {
            this.Time = time;
            this.ClassName = className;
            this.MethodName = methodName;
        }
        [YAXAttributeForClass()]
        [YAXSerializeAs("class")]
        public string ClassName { get; internal set; }

        [YAXAttributeForClass()]
        [YAXSerializeAs("time")]
        public long Time { get; internal set; }

        [YAXSerializeAs("method")]
        [YAXAttributeForClass()]
        public string MethodName { get; internal set; }
    }
}
