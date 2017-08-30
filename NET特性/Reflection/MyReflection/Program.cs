using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LogInterface;

namespace MyReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 不同程序集
            string assemblyPath = Path.Combine(Environment.CurrentDirectory, "LogTxt.dll");
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            if (assembly == null)
            {
                return;
            }

            try
            {
                string className = ConfigurationManager.AppSettings["LogType"].ToString();

                // 方法一
                ILog log = (ILog)assembly.CreateInstance(className);

                // 方法二
                //Type type = assembly.GetType(className);
                //ILog log = (ILog)type.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);

                // 调用
                log.Write(new Exception("异常测试"));
            }
            catch
            {
            }
            #endregion

        }
    }
}
