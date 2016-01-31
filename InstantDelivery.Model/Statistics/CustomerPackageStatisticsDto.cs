using System.Collections.Generic;

namespace InstantDelivery.Model.Statistics
{
    public class CustomerPackageStatisticsDto
    {
        public IList<MonthStatisticDto> MonthStatistics { get; set; } = new List<MonthStatisticDto>();
        public IList<WeekStatisticDto> WeekStatistics { get; set; } = new List<WeekStatisticDto>();
    }
}