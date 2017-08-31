using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTask
{
    public class TaskUsage3
    {
        void DoOnFirst()
        {
            Console.WriteLine("doing some task {0}", Task.CurrentId);
            Thread.Sleep(3000);
        }

        void DoOnSecond(Task t)
        {
            Console.WriteLine("preTask ID {0}", t.Id);
            Console.WriteLine("this task id {0}", Task.CurrentId);
            Console.WriteLine("do some cleanup");
            Thread.Sleep(3000);
        }



        public void TEST_TASK3()
        {
            //@Note Task.Run supports in .NET4.5 
            Task.Run(() => DoOnFirst()).ContinueWith((preTask) => DoOnSecond(preTask));

        }
    }
}
