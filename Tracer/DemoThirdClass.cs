using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TracerConsole
{
    public class DemoThirdClass
    {
        private Tracer fTracer;

        public DemoThirdClass(Tracer tracer)
        {
            fTracer = tracer;
        }

        public void ThirdMethod()
        {
            fTracer.StartTrace();
            Thread.Sleep(10);
            fTracer.StopTrace();
        }
    }
}
