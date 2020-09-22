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
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            try 
            {
                Logger.Info("Start");
                JsonSerializer jsonSerializer = new JsonSerializer();
                XMLSerializer xmlSerializer = new XMLSerializer();

                FileWriter fileWriter = new FileWriter();
                ConsoleWriter consoleWriter = new ConsoleWriter();

                Tracer tracer = new Tracer();
                DemoFirstClass demoFirstClass = new DemoFirstClass(tracer);
                DemoSecondClass demoSecondClass = new DemoSecondClass(tracer);
                DemoThirdClass demoThirdClass1 = new DemoThirdClass(tracer);
                demoFirstClass.FirstMethod();
                demoFirstClass.OtherMethod();
                demoThirdClass1.ThirdMethod();

                DemoThirdClass demoThirdClass = new DemoThirdClass(tracer);
                Thread myThread = new Thread(new ThreadStart(demoThirdClass.ThirdMethod));
                myThread.Start();

                DemoThirdClass demoThirdClass2 = new DemoThirdClass(tracer);
                Thread myThread2 = new Thread(new ThreadStart(demoThirdClass2.ThirdMethod));
                myThread2.Start();
                demoSecondClass.SecondMethod();
                string json = jsonSerializer.Serialize(tracer.GetTraceResult());
                string xml = xmlSerializer.Serialize(tracer.GetTraceResult());

                fileWriter.Write(json, xml);
                consoleWriter.Write(json, xml);

                Console.Read();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error!");
            }
        }
    }
}
