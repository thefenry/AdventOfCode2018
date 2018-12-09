using System;
using System.Collections.Generic;

namespace day_4
{
    class GuardActivityDetail
    {
        public string GuardId { get; set; }

        public DateTime ShiftStartDate { get; set; }

        public List<Activity> Activities { get; set; } = new List<Activity>();

        public int TotalTimeAsleep { get; set; }
    }
}
