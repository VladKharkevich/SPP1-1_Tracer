﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    interface ITracer
    {
        void StartTrace();
        
        void StopTrace();

        TraceResult GetTraceResult();
    }
}

