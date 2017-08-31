using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvent
{
    public class MyEventArgs : EventArgs
    {
        public string message
        {
            get;
            set;
        }

        public MyEventArgs(string mess)
        {
            message = mess;
        }
    }

    public class DoTest
    {
        public DoTest() { }

        //定义事件
        public event EventHandler<MyEventArgs> DoWork;


        public void Begin(string val)
        {
            if (DoWork != null)
            {
                MyEventArgs e = new MyEventArgs(val);
                DoWork(this, e);
            }
        }
    }

    class MyEventHandler
    {
        
        public void OnEvent(object sender, MyEventArgs e)
        {
            Console.WriteLine(e.message);
        }


        public void TEST_EVENTHANDLER()
        {
            DoTest dos = new DoTest();

            // 方式1：
            dos.DoWork += OnEvent;

            // 方:2：Lambda
            //dos.DoWork += (s, e) =>
            //{
            //    Console.WriteLine(e.message);
            //};
            

            // 触发事件
            dos.Begin("1");
            dos.Begin("2");
            dos.Begin("3");
            dos.Begin("4");
        }

    }

}
