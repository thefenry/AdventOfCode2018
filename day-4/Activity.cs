using System;

namespace day_4
{
    public class Activity
    {
        public Activity(int minute, string type, DateTime activityDate)
        {
            this.ActivityType = type;
            MinuteActivity = minute;
            ActivityDate = activityDate;
        }

        public int MinuteActivity { get; set; }

        public string ActivityType { get; set; }

        public DateTime ActivityDate { get; set; }
    }
}
