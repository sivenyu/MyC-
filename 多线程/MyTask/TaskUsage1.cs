using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace MyTask
{
    public class TaskUsage1
    {
        void LongRunTask(CancellationToken token)
        {

            //此处方法模拟一个耗时的工作 
            for (int i = 0; i < 1000; i++)
            {
                if (!token.IsCancellationRequested)
                {
                    Thread.Sleep(500);
                    Console.Write(".");
                }
                else
                {
                    Console.WriteLine("任务取消");
                    break;
                }
            }
        }

        void SubTask()
        {
            Task parant = new Task(() =>
            {
                new Task(() => Console.WriteLine("Hello")).Start();
                new Task(() => Console.WriteLine(",")).Start();
                new Task(() => Console.WriteLine("World")).Start();
                new Task(() => Console.WriteLine("!")).Start();
            });
            parant.Start();
            Console.ReadLine();
        }


        void TEST_TASK1()
        {
            //@note 验证子任务
            SubTask();


            //@note 验证任务取消
            //在.NET 4.0中使用Cooperative Cancellation模式，取消正在执行的任务变得非常简单
            CancellationTokenSource cts = new CancellationTokenSource();
            Task t = new Task(() => LongRunTask(cts.Token));
            t.Start();
            Thread.Sleep(2000);
            cts.Cancel();
            Console.Read();
        }
    }
}
