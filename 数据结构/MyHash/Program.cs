using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MyHash
{
    class MyHash
    {
        Hashtable m_AtmosphericPressureTable = new Hashtable();

        public void DoTest()
        {
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    int pressureVal = i * 10 + i;
                    string strKey = i.ToString();

                    List<int> pressureList = null;
                    if (m_AtmosphericPressureTable.Contains(strKey))
                    {
                        pressureList = m_AtmosphericPressureTable[strKey] as List<int>;
                    }
                    else
                    {
                        pressureList = new List<int>();
                        m_AtmosphericPressureTable.Add(strKey, pressureList);
                    }

                    pressureList.Add(pressureVal);
                }
            }
        }
    };



    class Program
    {
        static void Main(string[] args)
        {
            string str = "1-8通道温度设置为80摄氏度";
            string str2 = str.Trim("1-9".ToCharArray());
            if (str.IndexOf("温度设置为") != -1)
                Console.WriteLine("Found");
            else
                Console.WriteLine("NotFound");

            MyHash hash = new MyHash();
            hash.DoTest();

        }
    }
}
