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
        public string Serialize(RootResult rootResult)
        {
            YAXSerializer serializer = new YAXSerializer(typeof(RootResult));
            return serializer.Serialize(rootResult);
        }
    }
}
