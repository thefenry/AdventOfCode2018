using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace day_4
{     
    class BasicActivity
    {
        public DateTime ActivityDate { get; set; }

        public string ActivitySummary { get; set; }
    }

    //class GuardActivity
    //{
    //    public string Id { get; set; }

    //    public int? MinuteAsleep { get; set; }

    //    public int? MinuteWakingUp { get; set; }

    //    public bool WakesUp { get; set; } = false;

    //    public bool FallsAsleep { get; set; } = false;

    //    public bool BeginsShift { get; set; } = false;

    //    public DateTime ActivityDate { get; set; }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GuardSchedules.txt");
            string[] inputs = File.ReadAllLines(path);
            char[] timeSplitter = new char[] { '[', ']' };

            List<BasicActivity> guardActivities = new List<BasicActivity>();

            foreach (string activity in inputs)
            {
                string[] content = activity.Split(timeSplitter, StringSplitOptions.RemoveEmptyEntries);

                string[] guardInfoInput = content[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                BasicActivity guardInfo = new BasicActivity
                {
                    ActivityDate = DateTime.Parse(content[0]),
                    ActivitySummary = guardInfoInput[1]
                };


                //if (guardInfoInput[0] == "Guard")
                //{
                //    guardInfo.Id = guardInfoInput[1];
                //    guardInfo.BeginsShift = true;
                //}
                //else
                //{
                //    if (guardInfoInput[0] == "falls")
                //    {
                //        guardInfo.FallsAsleep = true;
                //        guardInfo.MinuteAsleep = guardInfo.ActivityDate.Minute;
                //    }
                //    else
                //    {
                //        guardInfo.WakesUp = true;
                //        guardInfo.MinuteWakingUp = guardInfo.ActivityDate.Minute;
                //    }
                //}

                guardActivities.Add(guardInfo);
            }

            var groupedGuardActivities = guardActivities.OrderBy(c => c.ActivityDate).GroupBy(c =>c.ActivityDate).ToList();

            //var guardSleepSchedule = new List<GuardActivity>();

            //foreach (var activity in guardActivities)
            //{


            //    if (!string.IsNullOrWhiteSpace(activity.Id))
            //    {
            //        var guardActivity = new GuardActivity
            //        {
            //            Id = activity.Id
            //        };


            //    }

            //    Console.WriteLine($"{activity.ActivityDate}-{activity.Id}-{activity.FallsAsleep}-{activity.WakesUp}");
            //}

            Console.ReadLine();
        }
    }
}
