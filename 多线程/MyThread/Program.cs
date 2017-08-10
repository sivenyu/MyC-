using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace TestThread
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 1、当在主线程中创建了一个线程，那么该线程的IsBackground默认是设置为FALSE的。
             * 2、当主线程退出的时候，IsBackground=FALSE的线程还会继续执行下去，直到线程执行结束。
             * 3、只有IsBackground=TRUE的线程才会随着主线程的退出而退出。
             * 4、当初始化一个线程，把Thread.IsBackground=true的时候，指示该线程为后台线程。后台线程将会随着主线程的退出而退出。
             * 5、只要所有前台线程都终止后，CLR就会对每一个活在的后台线程调用Abort（）来彻底终止应用程序。
             */

            /* 下面的代码示例对比了前台线程与后台线程的行为。
             * 创建一个前台线程和一个后台线程。前台线程使进程保持运行，直到它完成它的 while 循环。
             * 前台线程完成后，进程在后台线程完成它的 while 循环之前终止。
             */
            BackgroundTest shortTest = new BackgroundTest(50);
            Thread foregroundThread =  new Thread(new ThreadStart(shortTest.RunLoop));
            foregroundThread.Name = "ForegroundThread";

            BackgroundTest longTest = new BackgroundTest(10);
            Thread backgroundThread = new Thread(new ThreadStart(longTest.RunLoop));
            backgroundThread.Name = "BackgroundThread";
            backgroundThread.IsBackground = true;

            foregroundThread.Start();
            backgroundThread.Start();
            Console.WriteLine("Main() exist!");
        }
    }


    /*
     * .Net中的线程,可以分为后台线程和前台线程。后台线程与前台线程并没有本质的区别，它们之间唯一的区别就是:
     * 后台线程不会防止应用程序的进程被终止掉。呵呵,这句话读出来好像并不那么好懂.
     * 其实,说白了就是当前台线程都结束了的时候,整个程序也就结束了,即使还有后台线程正在运行,
     * 此时,所有剩余的后台线程都会被停止且不会完成.但是,只要还有一个前台线程没有结束,那么它将阻止程序结束.
     * 这就是为什么有些设计不够完美的WinForm程序，在某种特定的情况下，即使所有的窗口都关闭了，
     * 但是在任务管理器的管理列表里仍然可以找到该程序的进程，仍然在消耗着CPU和内存资源.
     * 因此,在WinForm程序中,关闭所有窗口前,应该停止所有前台线程,千万不要遗忘了某个前台线程.
     * 应用程序进程的存亡由前台线程决定而于后台线程无关.这就是它们的区别.
     */
    class BackgroundTest
    {
        int maxIterations;

        public BackgroundTest(int maxIterations)
        {
            this.maxIterations = maxIterations;
        }

        public void RunLoop()
        {
            String threadName = Thread.CurrentThread.Name;

            for (int i = 0; i < maxIterations; i++)
            {
                Console.WriteLine("{0} count: {1}", threadName, i.ToString());
                Thread.Sleep(250);
            }
            Console.WriteLine("{0} finished counting.", threadName);
        }
    }
}
