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
            Assembly a = Assembly.LoadFrom(assemblyPath);
            Type type = a.GetType(ConfigurationManager.AppSettings["LogType"].ToString());
            ILog log = (ILog)type.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
            log.Write(new Exception("异常测试"));
            #endregion

        }
    }
}
