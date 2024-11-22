using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public class Runner
    {
        protected List<BaseDay> days = new List<BaseDay>();

        public Runner(Assembly assembly = null)
        {
            //load from this assembly
            Assembly ass = Assembly.GetExecutingAssembly();
            //load from other assembly
            if (assembly != null)
                ass = Assembly.LoadFrom(assembly.GetName().Name);
            Console.WriteLine("Loading from {0}", ass.GetName().Name);

            //get all classes with baseday
            Type[] types = ass.GetTypes()
                           .Where(t => t.IsSubclassOf(typeof(BaseDay))).ToArray();

            //create instance of days
            days.Clear();
            foreach (Type type in types)
                days.Add((BaseDay)Activator.CreateInstance(type));

            //sort days
            List<BaseDay> sortedDays = new List<BaseDay>();
            for (int i = 0; i < days.Count; i++)
            {
                var d = days.FirstOrDefault(day => (day.Day == (i + 1).ToString()));
                sortedDays.Add(d);
            }
            days = sortedDays;
        }

        public void RunAll()
        {
            foreach (var item in days)
            {
                item.RunAllSolution1Tests();
                item.RunSolution1();
                item.RunAllSolution2Tests();
                item.RunSolution2();
            }
        }

        public void RunDay(int day)
        {
            days[day].RunAllSolution1Tests();
            days[day].RunSolution1();
            days[day].RunAllSolution2Tests();
            days[day].RunSolution2();
        }

        public void RunLatest()
        {
            int day = days.Count - 1;
            days[day].RunAllSolution1Tests();
            days[day].RunSolution1();
            days[day].RunAllSolution2Tests();
            days[day].RunSolution2();
        }

        public BaseDay GetDay(int day)
        {
            return days[day];
        }

        public BaseDay this[int index] 
        {
            get { return days[index]; }
            set { days[index] = value; }
        }
    }
}
