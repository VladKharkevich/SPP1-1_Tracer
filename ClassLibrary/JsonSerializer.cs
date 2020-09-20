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
        public string Serialize(RootResult rootResult)
        {
            string rawJson = JsonConvert.SerializeObject(rootResult);
            int i = 0;
            int counterOfTab = 0;
            while (i < rawJson.Length)
            {
                if (rawJson[i] == '{' || rawJson[i] == '[')
                {
                    counterOfTab++;
                    string tabs = "";
                    for (int j = 0; j < counterOfTab; j++)
                        tabs += "\t";
                    rawJson = rawJson.Substring(0, i + 1) + "\n" + tabs + rawJson.Substring(i + 1);
                }
                if (rawJson[i] == ',')
                {
                    string tabs = "";
                    for (int j = 0; j < counterOfTab; j++)
                        tabs += "\t";
                    rawJson = rawJson.Substring(0, i + 1) + "\n" + tabs + rawJson.Substring(i + 1);
                }
                if (rawJson[i] == '}' || rawJson[i] == ']')
                {
                    counterOfTab--;
                    string tabs = "";
                    for (int j = 0; j < counterOfTab - 1; j++)
                        tabs += "\t";
                    rawJson = rawJson.Substring(0, i + 1) + "\n" + tabs + rawJson.Substring(i + 1);
                }
                if (rawJson[i] == ':')
                    rawJson = rawJson.Substring(0, i + 1) + " " + rawJson.Substring(i + 1);
                i++;
            }
            return rawJson;
        }
    }
}
