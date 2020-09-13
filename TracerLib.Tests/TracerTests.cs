using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using FluentAssertions;

namespace TracerLib.Tests
{
    [TestClass]
    public class TracerTests
    {
        [TestMethod]
        public void TestTracerWhenRandomTimeSleep()
        {
            Random rnd = new Random();
            int ms = rnd.Next(1, 1001);
            Tracer tracer = new Tracer();
            tracer.StartTrace();
            Thread.Sleep(ms);
            tracer.StopTrace();
            tracer.GetTraceResult().Time.Should().BeGreaterOrEqualTo(ms);
            tracer.GetTraceResult().ClassName.Should().Be("TracerTests");
            tracer.GetTraceResult().MethodName.Should().Be("TestTracerWhenRandomTimeSleep");
        }
    }
}
