using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using LogInterface;

namespace LogTxt
{
    public class TextFileLog : ILog
    {
        public bool Write(string message)
        {
            string fileDir = ConfigurationManager.AppSettings["LogTarget"].ToString();
            using (StreamWriter w = File.AppendText(fileDir))
            {
                // w.Write(" Log Entry : ");
                w.WriteLine("发生时间{0}", DateTime.Now.ToLocalTime().ToString());
                w.WriteLine("日志内容为:{0}", message);
                w.WriteLine("-------------------------------");
                // Update the underlying file.
                w.Flush();
                w.Close();
            }
            return true;
        }
        public bool Write(Exception ex)
        {
            Write(ex.Message);
            return true;
        }

    }
}
