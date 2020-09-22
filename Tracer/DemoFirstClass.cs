using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TracerConsole
{
    public class DemoFirstClass
    {
        private Tracer fTracer;
        private DemoSecondClass fDemoSecondClass;

        public DemoFirstClass(Tracer tracer)
        {
            fTracer = tracer;
            fDemoSecondClass = new DemoSecondClass(fTracer);
        }

        public void FirstMethod()
        {
            fTracer.StartTrace();
            Thread.Sleep(100);
            fDemoSecondClass.SecondMethod();
            fTracer.StopTrace();
        }

        public void OtherMethod()
        {
            fTracer.StartTrace();
            Thread.Sleep(5);
            fTracer.StopTrace();
        }
    }
}
