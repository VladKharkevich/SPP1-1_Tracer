using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace TracerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("adsadsd");
            Tracer tracer = new Tracer();
            tracer.StartTrace();
            tracer.StopTrace();
            JsonSerializer jsonSerializer = new JsonSerializer();
            XMLSerializer xmlSerializer = new XMLSerializer();
            FileWriter fileWriter = new FileWriter();
            ConsoleWriter consoleWriter = new ConsoleWriter();

            string json = jsonSerializer.Serialize(tracer.GetTraceResult());
            string xml = xmlSerializer.Serialize(tracer.GetTraceResult());

            fileWriter.Write(json, xml);
            consoleWriter.Write(json, xml);

            Console.Read();
        }
    }
}
