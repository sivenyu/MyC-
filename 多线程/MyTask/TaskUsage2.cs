using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTask
{
    public class TaskUsage2
    {
        void TaskMethod()
        {
            Console.WriteLine("running in a task");
            Console.WriteLine("Task id: {0}", Task.CurrentId);
        }


        public void TEST_TASK2()
        {
            //方法1：使用TaskFactory
            //TaskFactory tf = new TaskFactory();
            //Task t1 = tf.StartNew(TaskMethod);

            //方法2：通过Task使用TaskFactory
            //Task t2 = Task.Factory.StartNew(TaskMethod);

            //方法3：使用构造函数和Start()
            Task t3 = new Task(TaskMethod);
            //t3.Start();
            t3.RunSynchronously();
        }
    }
}
