﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TestEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            //MyObserver.TEST_OBSERVER();



            MyEventHandler eTest = new MyEventHandler();
            eTest.TEST_EVENTHANDLER();


            Console.ReadKey();
        }
    }
}
