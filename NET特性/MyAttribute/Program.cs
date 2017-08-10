//#define DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Diagnostics;


namespace TestAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayRunningMessage();
            DisplayDebugMessage();

            MessageBox(0, "Hello", "Message", 0);

            Console.ReadLine();

        }


        //DllImport Attribute表明了MessageBox是User32.DLL中的函数，
        [DllImport("User32.dll")]
        public static extern int MessageBox(int hParent, string Message, string Caption, int Type);

        //Conditional对满足参数的定义条件的代码进行编译，如果没有定义DEBUG,那么该方法将不被编译
        [Conditional("DEBUG")]
        private static void DisplayRunningMessage()
        {
            Console.WriteLine("开始运行Main子程序。当前时间是" + DateTime.Now);
        }

        //Obsolete表明了DispalyDebugMessage方法已经过时了，它有一个更好的方法来代替它，
        //当我们的程序调用一个声明了Obsolete的方法时，那么编译器会给出信息，Obsolete还有其他两个重载的版本
        //[Conditional("DEBUG")]
        [Obsolete]
        private static void DisplayDebugMessage()
        {
            Console.WriteLine("开始Main子程序");
        }

    }
}
