using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer tmr = new Timer(Tick, "tick...", 1000, 1000);
            Console.ReadLine();
            tmr.Dispose();

            Console.ReadKey();
        }

        static void Tick(object data)
        {
            Console.WriteLine(data);
            Thread.Sleep(2000);
            Console.Write("Tack");
        }
    }
}
