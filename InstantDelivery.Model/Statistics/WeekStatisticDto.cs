using System;

namespace InstantDelivery.Model.Statistics
{
    public class WeekStatisticDto
    {
        public DayOfWeek Day { get; set; }
        public decimal Cost { get; set; }
        public int Count { get; set; }
    }
}