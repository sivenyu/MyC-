using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyList
{
    interface IMyinterface
    {
        void Do();
    }
    struct mystruct : IMyinterface
    {
        void IMyinterface.Do()
        {

        }
    }

    public class Program
    {
        #region 测试Distinct
        public class Person
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public Person(int id)
            {
                this.ID = id;
            }
        }

        public delegate bool EqualsComparer<T>(T x, T y);

        public class Compare<T> : IEqualityComparer<T>
        {
            private EqualsComparer<T> _equalsComparer;

            public Compare(EqualsComparer<T> equalsComparer)
            {
                this._equalsComparer = equalsComparer;
            }

            public bool Equals(T x, T y)
            {
                if (null != this._equalsComparer)
                    return this._equalsComparer(x, y);
                else
                    return false;
            }

            public int GetHashCode(T obj)
            {
                return obj.ToString().GetHashCode();
            }
        }
        static void TEST_DISTINCT()
        {
            List<Person> personList = new List<Person>(){
                new Person(3),//重复数据
                new Person(3),
                new Person(2),
                new Person(1)
            };

            //注意：
            //调用Distinct同时必须用给自己或另外List对象赋值，否则不会产生动作

            //使用匿名方法
            List<Person> delegateList = personList.Distinct(new Compare<Person>(
                delegate (Person x, Person y)
                {
                    if (null == x || null == y) return false;
                    return x.ID == y.ID;
                })).ToList();

            delegateList.ForEach(s => Console.WriteLine(s.ID));

            //使用 Lambda 表达式
            List<Person> lambdaList = personList.Distinct(new Compare<Person>(
                (x, y) => (null != x && null != y) && (x.ID == y.ID))).ToList();

            lambdaList.ForEach(s => Console.WriteLine(s.ID));

            //排序
            personList.Sort((x, y) => x.ID.CompareTo(y.ID));
            personList.ForEach(s => Console.WriteLine(s.ID));
        }

        #endregion


        #region remove
        static  void ShowList(string title, List<int> theList)
        {
            Console.WriteLine($"\r\n{title}（{theList.Count}）:");
            foreach(var i in theList)
            {
                Console.Write($"{i.ToString()} ");
            }
        }
        static void TEST_REMOVE()
        {
            List<int> integers = new List<int>();

            // 初始化
            for (int i = 0; i < 10; i++)
            {
                integers.Add(i);
            }
            for (int i = 0; i < 10; i++)
            {
                integers.Add(i);
            }
            ShowList("当前列表", integers);

            // [1] 这个会跑出异常
            // 因为foreach中删除元素时，每一次删除都会导致集合的大小和元素索引值发生变化，
            // 从而导致在foreach中删除元素时会抛出异常。
            //foreach (var i in integers)
            //{
            //    integers.Remove(i);
            //}

            // [2] 这个循环相当于把原来偶数序号的元素删除掉
            //for (int i = 0; i < integers.Count; i++)
            //{
            //    integers.Remove(integers[i]);
            //}

            // [3] 这样从高往低顺序就可以全部删除完毕,OK!
            //for (int i = integers.Count - 1; i >= 0; i--)
            //{
            //    integers.Remove(integers[i]);
            //}

            // 从头开始遍历，每删除一个必须后退一位才能删干净
            for (int i = 0; i < integers.Count; i++)
            {
                if (integers[i] == 5 || integers[i] == 6)
                {
                    integers.Remove(integers[i]);
                    i--; // 必须后退一位
                }
            }

            // 这个测试把前面重叠的元素删除掉
            //for (int i = integers.Count - 1; i >= 0; i--)
            //{
            //    for (int j = 0; j < i; j++)
            //    {
            //        if (integers[j] == integers[i])
            //        {
            //            integers.Remove(integers[j]);
            //            i--;
            //        }
            //    }
            //}


            ShowList("删除后的列表", integers);
        }
        #endregion


        static void Main(string[] args)
        {
            //TEST_DISTINCT();
            TEST_REMOVE();


            Console.ReadKey();
        }
    }
}
