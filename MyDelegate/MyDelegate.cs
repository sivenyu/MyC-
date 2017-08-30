using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyDelegate
{
    public delegate int MethodDelegate(int x, int y);


    public class MyDelegate
    {
        private MethodDelegate method;

        private int Add(int x, int y)
        {
            Thread.Sleep(2000);
            return x + y;
        }

        private void AddCompeleted(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            MethodDelegate method = ar.AsyncState as MethodDelegate;
            Trace.Assert(method != null, "Invalid object type");

            int sum = method.EndInvoke(ar);
            Console.WriteLine($"Call delegate method result:{sum}");
        }

        public void TEST_MYDELEGATE()
        {

            //方法1：原始的委托实例化
            //method = new MethodDelegate(Add);

            //方法2：.NET2.0的委托实例化
            //method = Add;
            //int sum = method(2, 3);

            //方法3：匿名函数代替Add方法定义
            //method = new MethodDelegate(delegate (int x, int y) { return x + y; });

            //方法4：.NET2.0的匿名函数代替Add方法定义
            //method = delegate (int x, int y) { return x + y; };

            //方法5：lambda表达式
            method = Add;
            method.BeginInvoke(2, 3,
                ar => 
                {
                    int ret = method.EndInvoke(ar);
                    Console.WriteLine($"Call delegate method result:{ret}");
                }, null);



            int sum = 0;

            //调用方法1：直接调用
            //sum = method(2, 3);

            //调用方法2：轮询
            //IAsyncResult ar = method.BeginInvoke(2, 3, null, null);
            //while(!ar.IsCompleted)
            //{
            //    Console.Write(".");
            //    Thread.Sleep(50);
            //}
            //sum = method.EndInvoke(ar);

            //调用方法3：等待句柄
            //IAsyncResult ar = method.BeginInvoke(2, 3, null, null);
            //while (true)
            //{
            //    Console.Write(".");
            //    if(ar.AsyncWaitHandle.WaitOne(50, false))
            //    {
            //        Console.Write(".");
            //        break;
            //    }
            //}
            //sum = method.EndInvoke(ar);

            //调用方法4：异步回调
            //method.BeginInvoke(2, 3, AddCompeleted, method);



            Console.WriteLine($"Call delegate method result:{sum}");
            Console.ReadKey();
        }
    }
}
