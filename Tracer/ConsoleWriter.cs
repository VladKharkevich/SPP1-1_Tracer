using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerConsole
{
    public class ConsoleWriter : IWriterResults
    {
        public void Write(params string[] args)
        {
            foreach (string str in args)
            {
                Console.WriteLine(str);
            }
        }
    }
}
