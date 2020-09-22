using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using FluentAssertions;
using TracerConsole;

namespace TracerLib.Tests
{
    [TestClass]
    public class TracerTests
    {
        Tracer tracer;
        DemoFirstClass demoFirstClass;
        DemoSecondClass demoSecondClass;
        DemoThirdClass demoThirdClass;

        [TestInitialize]
        public void TestInit()
        {
            tracer = new Tracer();
            demoFirstClass = new DemoFirstClass(tracer);
            demoSecondClass = new DemoSecondClass(tracer);
            demoThirdClass = new DemoThirdClass(tracer);
        }

        [TestMethod]
        public void TestSumOfTimeInOneThread()
        {
            demoFirstClass.FirstMethod();
            demoSecondClass.SecondMethod();
            demoThirdClass.ThirdMethod();
            long sumTime = 0;
            foreach (MethodResult methodResult in tracer.GetTraceResult().Threads[0].DependenceMethods)
            {
                sumTime += methodResult.Time;
            }
            sumTime.Should().Be(tracer.GetTraceResult().Threads[0].Time);
        }

        [TestMethod]
        public void TestCountOfThreads()
        {
            int testingCountOfThreads = 3;
            Thread myThread2 = new Thread(new ThreadStart(demoSecondClass.SecondMethod));
            myThread2.Start();
            Thread myThread3 = new Thread(new ThreadStart(demoThirdClass.ThirdMethod));
            myThread3.Start();
            Thread.Sleep(20);       //Solve problem with data race 
            tracer.GetTraceResult().Threads.Count.Should().Be(testingCountOfThreads);
        }

        [TestMethod]
        public void TestCountOfMethods()
        {
            int testingCountOfMethods = 3;
            demoSecondClass.SecondMethod();
            demoThirdClass.ThirdMethod();
            demoFirstClass.OtherMethod();
            tracer.GetTraceResult().Threads[0].DependenceMethods.Count.Should().Be(testingCountOfMethods);
        }

        [TestMethod]
        public void TestClassName()
        {
            demoFirstClass.FirstMethod();
            tracer.GetTraceResult().Threads[0].DependenceMethods[0].ClassName.Should().Be("DemoFirstClass");
        }

        [TestMethod]
        public void TestMethodName()
        {
            demoSecondClass.SecondMethod();
            tracer.GetTraceResult().Threads[0].DependenceMethods[0].MethodName.Should().Be("SecondMethod");
        }
    }
}
