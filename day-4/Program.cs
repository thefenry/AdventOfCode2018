using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace day_4
{
    class GuardsMinutesDetails
    {
        public int GuardId { get; set; }
        public Dictionary<int, int> GuardMinuteAsleep { get; set; }
    }

    class Program
    {
        private static bool foundNumber;
        private static List<BasicActivity> _sortedGuardActivities;
        private static List<IGrouping<string, GuardActivityDetail>> _groupedGuards;

        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GuardSchedules.txt");
            string[] inputs = File.ReadAllLines(path);
            _sortedGuardActivities = ParseAndSortGuardData(inputs);

            GetCommonSleepMinuteIdMultiple();

            GuardMinuteFrequencyIdMultiple();

            Console.ReadLine();
        }

        private static void GuardMinuteFrequencyIdMultiple()
        {
            List<GuardsMinutesDetails> guardsMinutesDetails = GetGuardsMinutesAsleep();

            int guardId = 0;
            int highestFrequencyMin = -1;
            int frenquencyValue = -1;

            foreach (GuardsMinutesDetails guardsMinutesDetail in guardsMinutesDetails)
            {

                guardsMinutesDetail.GuardMinuteAsleep = guardsMinutesDetail.GuardMinuteAsleep.OrderBy(x => x.Key).ToDictionary(x => x.Key, c => c.Value);

                foreach (KeyValuePair<int, int> item in guardsMinutesDetail.GuardMinuteAsleep)
                {

                    if (item.Value > frenquencyValue)
                    {
                        //int frenqValuen = frenquencyValue;
                        frenquencyValue = item.Value;
                        highestFrequencyMin = item.Key;
                        guardId = guardsMinutesDetail.GuardId;

                    }
                }

            }

            Console.WriteLine(highestFrequencyMin * guardId);

        }

        private static List<GuardsMinutesDetails> GetGuardsMinutesAsleep()
        {
            List<GuardsMinutesDetails> guardsMinutesDetails = new List<GuardsMinutesDetails>();

            for (int i = 0; i < _groupedGuards.Count; i++)
            {
                IGrouping<string, GuardActivityDetail> guard = _groupedGuards[i];

                GuardsMinutesDetails newGuard = new GuardsMinutesDetails
                {
                    GuardId = int.Parse(guard.Key.Split('#', StringSplitOptions.RemoveEmptyEntries)[0]),
                    GuardMinuteAsleep = new Dictionary<int, int>()
                };

                List<GuardActivityDetail> guardActivitiesList = guard.ToList();

                for (int j = 0; j < guardActivitiesList.Count; j++)
                {
                    List<Activity> activities = guardActivitiesList[j].Activities;

                    for (int k = 0; k < activities.Count; k++)
                    {
                        Activity guardActivity = activities[k];

                        if (guardActivity.ActivityType == "falls")
                        {
                            for (int l = guardActivity.MinuteActivity; l < activities[k + 1].MinuteActivity + 1; l++)
                            {
                                if (newGuard.GuardMinuteAsleep.ContainsKey(l))
                                {
                                    newGuard.GuardMinuteAsleep[l] += 1;
                                }
                                else
                                {
                                    newGuard.GuardMinuteAsleep.Add(l, 1);
                                }
                            }
                        }
                    }
                }
                guardsMinutesDetails.Add(newGuard);
            }

            return guardsMinutesDetails;
        }

        private static void GetCommonSleepMinuteIdMultiple()
        {
            List<GuardActivityDetail> guardActivityDetails = GetGuardsShiftActivitySchedule(_sortedGuardActivities);

            _groupedGuards = guardActivityDetails.GroupBy(c => c.GuardId).ToList();

            List<GuardActivityDetail> guardWithMostAsleepTime = GetGuardWithMostAsleepTime(_groupedGuards);

            int guardId = int.Parse(guardWithMostAsleepTime.First().GuardId.Split('#', StringSplitOptions.RemoveEmptyEntries)[0]);

            List<Activity> activities = guardWithMostAsleepTime.SelectMany(c => c.Activities).ToList();

            for (int i = 0; i < activities.Count; i++)
            {
                Activity activity = activities[i];

                for (int j = 0; j < activities.Count; j++)
                {
                    Activity activityToCheckAgainst = activities[j];
                    if (activityToCheckAgainst.ActivityDate.Day != activity.ActivityDate.Day && activity.ActivityType == "falls")
                    {
                        if (activity.MinuteActivity <= activityToCheckAgainst.MinuteActivity)
                        {
                            if (activities[i + 1].MinuteActivity >= activityToCheckAgainst.MinuteActivity)
                            {
                                int range2Min = activityToCheckAgainst.MinuteActivity;

                                // answer 67558
                                Console.WriteLine(range2Min * guardId);
                                foundNumber = true;
                                break;
                            }
                        }
                    }
                }

                if (foundNumber)
                {
                    break;
                }
            }
        }

        private static List<BasicActivity> ParseAndSortGuardData(string[] inputs)
        {
            List<BasicActivity> guardActivities = ParseBasicActivty(inputs);

            List<BasicActivity> _sortedGuardActivities = guardActivities.OrderBy(c => c.ActivityDate).ToList();
            return _sortedGuardActivities;
        }

        private static List<GuardActivityDetail> GetGuardWithMostAsleepTime(List<IGrouping<string, GuardActivityDetail>> groupedGuards)
        {
            string guardId = string.Empty;
            int guardTotalTimeAsleep = 0;
            List<GuardActivityDetail> guardInfoWithMostSleepTime = new List<GuardActivityDetail>();

            for (int i = 0; i < groupedGuards.Count; i++)
            {
                IGrouping<string, GuardActivityDetail> guard = groupedGuards[i];

                int totalTimeAsleep = 0;

                List<GuardActivityDetail> guardActivitiesList = guard.ToList();

                for (int j = 0; j < guardActivitiesList.Count; j++)
                {
                    totalTimeAsleep += guardActivitiesList[j].TotalTimeAsleep;
                }

                if (string.IsNullOrEmpty(guardId))
                {
                    guardId = guard.Key;
                    guardTotalTimeAsleep = totalTimeAsleep;
                }
                else
                {
                    if (guardTotalTimeAsleep < totalTimeAsleep)
                    {
                        guardId = guard.Key;
                        guardTotalTimeAsleep = totalTimeAsleep;
                    }
                }

                if (guard.Key == guardId)
                {
                    guardInfoWithMostSleepTime = guardActivitiesList;
                }
            }

            return guardInfoWithMostSleepTime;
        }

        private static List<BasicActivity> ParseBasicActivty(string[] inputs)
        {
            char[] timeSplitter = new char[] { '[', ']' };

            List<BasicActivity> guardActivities = new List<BasicActivity>();

            foreach (string activity in inputs)
            {
                string[] content = activity.Split(timeSplitter, StringSplitOptions.RemoveEmptyEntries);

                string[] guardInfoInput = content[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                BasicActivity guardInfo = new BasicActivity
                {
                    ActivityDate = DateTime.Parse(content[0]),
                    ActivitySummary = content[1]
                };

                guardActivities.Add(guardInfo);
            }

            return guardActivities;
        }

        private static List<GuardActivityDetail> GetGuardsShiftActivitySchedule(List<BasicActivity> sortedGuardActivities)
        {
            List<GuardActivityDetail> guardActivityDetails = new List<GuardActivityDetail>();

            foreach (BasicActivity guardActivity in sortedGuardActivities)
            {
                string[] guardActivityFragment = guardActivity.ActivitySummary.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (guardActivityFragment[0] == "Guard")
                {
                    GuardActivityDetail guardDetail = new GuardActivityDetail
                    {
                        GuardId = guardActivityFragment[1],
                        ShiftStartDate = guardActivity.ActivityDate
                    };

                    guardActivityDetails.Add(guardDetail);
                }
                else
                {
                    if (guardActivityFragment[0] == "falls")
                    {
                        GuardActivityDetail guardDetail = guardActivityDetails.Last();
                        guardDetail.Activities.Add(new Activity(guardActivity.ActivityDate.Minute, "falls", guardActivity.ActivityDate));

                    }
                    else
                    {
                        GuardActivityDetail guardDetail = guardActivityDetails.Last();
                        guardDetail.Activities.Add(new Activity(guardActivity.ActivityDate.Minute, "wakes", guardActivity.ActivityDate));

                        int activitiesCount = guardDetail.Activities.Count;
                        guardDetail.TotalTimeAsleep += guardDetail.Activities[activitiesCount - 1].MinuteActivity - guardDetail.Activities[activitiesCount - 2].MinuteActivity;
                    }
                }
            }

            return guardActivityDetails;
        }
    }
}
