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
            Console.WriteLine(tracer.GetTraceResult().ClassName);
            Console.Read();
        }
    }
}
