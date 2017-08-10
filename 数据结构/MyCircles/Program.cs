using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MyCircles
{

    public class CircleObj
    {
        public string CircleNo { get; set; }
        public readonly int CircleBlockNo;

        public List<StepObj> Steps;

        public CircleObj(string circleNo, int circleBlockNo)
        {
            this.CircleNo = circleNo;
            this.CircleBlockNo = circleBlockNo;
            Steps = new List<StepObj>();
        }
    }

    public class StepObj
    {
        public int StepNo { get; private set; }
        public string WorkTypeDesc { get; set; }
        public string Text
        {
            get { return string.Format("{0}-{1}", StepNo, WorkTypeDesc); }
        }

        public StepObj(int stepNo)
        {
            this.StepNo = stepNo;
        }
    }



    public class Paser
    {
        public string GetStepDesc(int StepNo)
        {
            if (stepName.Contains(StepNo))
                return (string)stepName[StepNo];
            else
                return "";
        }

        public void GetCircleNoStepNo(string key, out string circleNo, out int stepNo)
        {
            string[] keyArray = key.Split('-');
            circleNo = keyArray[1];
            stepNo = Convert.ToInt32(keyArray[keyArray.Length - 1]);
        }
        public void GetCircleBlockAndCircleNo(string key, out string blockNo, out string circleNo, out string stepNo)
        {
            blockNo = "00";
            circleNo = "0000";
            stepNo = "000000";
            string[] keyArray = key.Split('-');
            blockNo = keyArray[0];
            circleNo = keyArray[1];
            stepNo = keyArray[keyArray.Length - 1];
        }
        
        private bool IsNewCircle(string prevKey, string key, out bool newBlockNo)
        {
            newBlockNo = false;
            if (String.IsNullOrEmpty(prevKey))
            {
                newBlockNo = true;
                return true;
            }
            string preBlockNo, preCircleNo, preStepNo, blockNo, circleNo, stepNo;
            GetCircleBlockAndCircleNo(prevKey, out preBlockNo, out preCircleNo, out preStepNo);
            GetCircleBlockAndCircleNo(key, out blockNo, out circleNo, out stepNo);
            if (preBlockNo != blockNo) //不同块号的肯定是新的循环
            {
                newBlockNo = true;
                return true;
            }
            else
            {
                if (preCircleNo != circleNo)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        
        private int GetOrderOfStepinCircle(string key)
        {
            string[] keyArray = key.Split('-');
            if (keyArray.Length < 4+3)
                return 0;

            const string Empty = "00000:00000";
            if (keyArray[5] != Empty)
                return 4;
            if (keyArray[4] != Empty)
                return 3;
            if (keyArray[3] != Empty)
                return 2;
            if (keyArray[2] != Empty)
                return 1;
            return 0;
        }

        Hashtable stepName = new Hashtable();
        List<CircleObj> circleObjs = new List<CircleObj>();
        List<string> keyList = new List<string>();
        public void Init()
        {
            stepName.Add(1, "搁置");
            stepName.Add(2, "恒流充");
            stepName.Add(3, "恒压充");
            stepName.Add(4, "GOTO 2");
            stepName.Add(5, "搁置");
            stepName.Add(6, "GOTO 1");
            stepName.Add(7, "工步结束");

            keyList.Add("00-0000-00000:00000-00000:00000-00000:00000-00000:00000-001");
            keyList.Add("00-0000-00000:00000-00000:00000-00000:00000-00000:00000-002");
            keyList.Add("00-0000-00000:00000-00000:00000-00000:00000-00000:00000-003");
            keyList.Add("00-0000-00003:00001-00000:00000-00000:00000-00000:00000-002");
            keyList.Add("00-0000-00003:00001-00000:00000-00000:00000-00000:00000-003");
            keyList.Add("00-0000-00000:00000-00000:00000-00000:00000-00000:00000-005");
 
            keyList.Add("00-0000-00000:00000-00005:00001-00000:00000-00000:00000-001");
            keyList.Add("00-0000-00000:00000-00005:00001-00000:00000-00000:00000-002");
            keyList.Add("00-0000-00000:00000-00005:00001-00000:00000-00000:00000-003");
            keyList.Add("00-0000-00003:00001-00005:00001-00000:00000-00000:00000-002");
            keyList.Add("00-0000-00003:00001-00005:00001-00000:00000-00000:00000-003");
            keyList.Add("00-0000-00000:00000-00005:00001-00000:00000-00000:00000-005");

            keyList.Add("00-0000-00000:00000-00000:00000-00000:00000-00000:00000-007");
        }

        public void Run()
        {
            string previousCircleNo = "";
            int circleRowHandle = 0, stepRowHandle = 0;
            int circleBlockNo = 0;
            CircleObj circleObj = null;

            List<StepObj>[] tmpStep = new[] { new List<StepObj>(), new List<StepObj>(), new List<StepObj>(), new List<StepObj>() };
            

            foreach (string key in keyList)
            {
                string circleNo;//循环次数
                int stepNo;     //工步号
                bool newBlockNo;
                GetCircleNoStepNo(key, out circleNo, out stepNo);
                if (IsNewCircle(previousCircleNo, key, out newBlockNo))
                {
                    // 添加新循环
                    if (newBlockNo)
                        circleBlockNo++;
                    circleObj = new CircleObj(circleNo, circleBlockNo);
                    //circleObj.RowHandle = circleRowHandle++
                    
                    //_allLayerTrace.Add(circleObj.RowHandle, allTotalRecordIndex);
                    stepRowHandle = 0;
                    //circleTotalRecordIndex = 0;
                    circleObjs.Add(circleObj);
                }

                // 添加工步
                previousCircleNo = key;
                StepObj step = new StepObj(stepNo);
                step.WorkTypeDesc = GetStepDesc(stepNo);

                int Order = GetOrderOfStepinCircle(key);
                tmpStep[Order].Add(step);
                //circleObj.Steps.Add(step);
            }
        }

        public void Print()
        {
            foreach (var circle in circleObjs)
            {
                Console.WriteLine("Circle[{0}], {1}:", circle.CircleBlockNo, circle.CircleNo);
                foreach (var step in circle.Steps)
                {
                    Console.WriteLine("\t{0}", step.Text);
                }
            }

            Console.WriteLine("------------------------");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Paser sr = new Paser();
            sr.Init();
            sr.Run();
            sr.Print();

            Console.ReadKey();
        }
    }
}
