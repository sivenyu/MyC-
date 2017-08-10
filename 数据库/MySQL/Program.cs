using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQL
{
    class Program
    {
        static void Main(string[] args)
        {

            TEST_LOGIN();


            Console.WriteLine("Any key to exist...");
            Console.ReadKey();
        }


        static void TEST_LOGIN()
        {
            using (SqlHelper sql = new SqlHelper())
            {
                if (sql.Login("操作员", "."))
                    Console.WriteLine("登陆成功！");
                else
                    Console.WriteLine("登陆失败！");
            }
        }
    }
}
