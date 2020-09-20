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
            JsonSerializer jsonSerializer = new JsonSerializer();
            XMLSerializer xmlSerializer = new XMLSerializer();

            FileWriter fileWriter = new FileWriter();
            ConsoleWriter consoleWriter = new ConsoleWriter();

            Tracer tracer = new Tracer();
            DemoFirstClass demoFirstClass = new DemoFirstClass(tracer);
            demoFirstClass.FirstMethod();

            DemoThirdClass demoThirdClass = new DemoThirdClass(tracer);
            Thread myThread = new Thread(new ThreadStart(demoThirdClass.SecondMethod));
            myThread.Start();

            string json = jsonSerializer.Serialize(tracer.GetTraceResult());
            string xml = xmlSerializer.Serialize(tracer.GetTraceResult());

            fileWriter.Write(json, xml);
            consoleWriter.Write(json, xml);

            Console.Read();
        }
    }
}
