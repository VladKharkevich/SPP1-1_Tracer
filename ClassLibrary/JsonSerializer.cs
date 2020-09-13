using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClassLibrary
{
    public class JsonSerializer : ISerialize
    {
        public string Serialize(TraceResult traceResult)
        {
            return JsonConvert.SerializeObject(traceResult);
        }
    }
}
