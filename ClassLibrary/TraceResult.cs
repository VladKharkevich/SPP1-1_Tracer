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

        public long Time { get; internal set; }
        public string ClassName { get; internal set; }
        public string MethodName { get; internal set; }
    }
}
