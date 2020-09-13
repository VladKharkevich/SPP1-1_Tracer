using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using YAXLib;

namespace ClassLibrary
{
    public class XMLSerializer : ISerialize
    {
        public string Serialize(TraceResult traceResult)
        {
            YAXSerializer serializer = new YAXSerializer(typeof(TraceResult));
            return serializer.Serialize(traceResult);
        }
    }
}
