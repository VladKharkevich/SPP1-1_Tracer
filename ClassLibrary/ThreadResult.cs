﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace ClassLibrary
{
    public class ThreadResult
    {
        public ThreadResult(int id,  long time)
        {
            this.Time = time;
            this.Id = id;
            this.DependenceMethods = new List<MethodResult>();
        }
        [YAXAttributeForClass()]
        [YAXSerializeAs("id")]
        public int Id { get; internal set; }

        [YAXAttributeForClass()]
        [YAXSerializeAs("time")]
        public long Time { get; internal set; }

        [YAXAttributeForClass()]
        [YAXSerializeAs("methods")]
        public List<MethodResult> DependenceMethods { get; internal set; }
    }
}
