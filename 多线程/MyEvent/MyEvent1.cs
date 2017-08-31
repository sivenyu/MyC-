using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestEvent
{
    class MyEvent1
    {
        ManualResetEvent mEvent = new ManualResetEvent(false);

        public void DoThread()
        {
            Console.WriteLine("thread: Waiting an event");
            bool bRet = mEvent.WaitOne(1000 * 2);
            if (bRet)
                Console.WriteLine("thread: got an event");
            else
                Console.WriteLine("thread: timeout");
        }

        public void Run()
        {
            mEvent.Reset();
            Thread thrd1 = new Thread(new ThreadStart(DoThread));
            thrd1.Name = "thread1";
            thrd1.Start();

            for (int x = 0; x < 10; x++)
            {
                Thread.Sleep(500);
                Console.WriteLine("{0}.", x);
            }
            mEvent.Set();

            while (thrd1.IsAlive)
            {
                Thread.Sleep(1000);
                Console.WriteLine("waiting for thread to stop");
            }
        }


        public static void TEST_EVENT1()
        {
            MyEvent1 myClass = new MyEvent1();
            Console.WriteLine("\r\nFirst run>>");
            myClass.Run();
            Console.WriteLine("\r\nFirst run<<");

            //Console.WriteLine("\r\nSecond run>>");
            //myClass.Run();
            //Console.WriteLine("\r\nSecond run<<");

            Console.ReadKey();
        }
}
}
