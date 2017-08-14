using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInterface
{
    public interface ILog
    {
        bool Write(string message);
        bool Write(Exception ex);
    }
}
