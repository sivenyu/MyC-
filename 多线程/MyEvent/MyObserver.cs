using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvent
{
    // 定义委托
    public delegate void NumberChangedEventHandler(int count);

    // 定义事件发布者
    public class Publishser
    {
        private int count;
        //public NumberChangedEventHandler NumberChanged;       // 声明委托变量
        public event NumberChangedEventHandler NumberChanged;   // 声明一个事件

        public void DoSomething()
        {
            // 在这里完成一些工作 ...

            if (NumberChanged != null)                          // 触发事件
            {
                count++;
                NumberChanged(count);
            }
        }
    }

    // 定义事件订阅者
    public class Subscriber
    {
        public void OnNumberChanged(int count)
        {
            Console.WriteLine("Subscriber notified: count = {0}", count);
        }
    }



    public class MyObserver
    {
        public static void TEST_OBSERVER()
        {
            Publishser pub = new Publishser();
            Subscriber sub = new Subscriber();

            pub.NumberChanged += new NumberChangedEventHandler(sub.OnNumberChanged);
            pub.DoSomething();          // 应该通过DoSomething()来触发事件
            //pub.NumberChanged(100);     // 但可以被这样直接调用，对委托变量的不恰当使用
        }
    }
}
