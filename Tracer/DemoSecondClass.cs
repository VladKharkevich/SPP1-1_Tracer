﻿using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TracerConsole
{
    public class DemoSecondClass
    {
        private Tracer fTracer;

        internal DemoSecondClass(Tracer tracer)
        {
            fTracer = tracer;
        }

        public void SecondMethod()
        {
            fTracer.StartTrace();
            Thread.Sleep(100);
            fTracer.StopTrace();
        }
    }
}
