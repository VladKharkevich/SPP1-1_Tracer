using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using FluentAssertions;
using TracerConsole;

namespace TracerLib.Tests
{
    [TestClass]
    public class SerializersTests
    {
        Tracer tracer;
        DemoSecondClass demoSecondClass;
        long time;
        string className;
        string methodName;

        [TestInitialize]
        public void TestInit()
        {
            tracer = new Tracer();
            demoSecondClass = new DemoSecondClass(tracer);
            demoSecondClass.SecondMethod();
            time = tracer.GetTraceResult().Threads[0].DependenceMethods[0].Time;
            className = tracer.GetTraceResult().Threads[0].DependenceMethods[0].ClassName;
            methodName = tracer.GetTraceResult().Threads[0].DependenceMethods[0].MethodName;
        }

        [TestMethod]
        public void TestXMLSerializer()
        {
            XMLSerializer xmlSerializer = new XMLSerializer();
            string xml = xmlSerializer.Serialize(tracer.GetTraceResult());
            xml.Should().Contain($"<MethodResult class=\"{className}\" time=\"{time}\" methodName=\"{methodName}\">");
        }
        [TestMethod]
        public void TestJsonSerializer()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            string json = jsonSerializer.Serialize(tracer.GetTraceResult());
            json.Should().Contain($"\"ClassName\": \"{className}\"");
            json.Should().Contain($"\"Time\": {time}");
            json.Should().Contain($"\"MethodName\": \"{methodName}\"");
        }
    }
}
