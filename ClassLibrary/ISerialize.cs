﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    interface ISerialize
    {
        string Serialize(TraceResult traceResult);
    }
}
