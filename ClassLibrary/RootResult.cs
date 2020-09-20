using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace ClassLibrary
{
    public class RootResult
    {
        public RootResult(int defaultNumber = 0)
        {
            this.Threads = new List<ThreadResult>();
        }

        [YAXAttributeForClass()]
        [YAXSerializeAs("threads")]
        public List<ThreadResult> Threads { get; internal set; }
    }
}
