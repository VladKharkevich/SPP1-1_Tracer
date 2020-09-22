using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TracerConsole
{
    public class FileWriter : IWriterResults
    {
        public void Write(params string[] args)
        {
            var path = "JSON_XML.txt";
            if (File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (string str in args)
                        sw.WriteLine(str);
                }
            }
        }
    }
}
