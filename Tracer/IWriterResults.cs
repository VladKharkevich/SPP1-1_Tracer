using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerConsole
{
    interface IWriterResults
    {
        void Write(params string[] args);
    }
}
