using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFunc
{
    public class MyFunc1
    {

        Func<int, bool> myFunc = null;//全部变量

        private bool CheckIsInt32(int pars)//被封装的方法
        {
            return pars == 5;
        }



        public void TEST_FUNC1()
        {
            myFunc = x => CheckIsInt32(x);//给委托封装方法的地方 使用了Lambda表达式

            bool ok = myFunc(5);//调用委托
        }
    }
}
