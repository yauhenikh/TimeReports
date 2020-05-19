using System;
using System.Collections.Generic;

namespace TimeReports.Services.Entities
{
    public class DailyHoursViewModel
    {
        public DayOfWeek Day { get; set; }
        public List<EmployeeHoursViewModel> EmployeeAverageHours { get; set; }

        public override string ToString()
        {   
            return $"| {Day, -9} | " + EmployeeAverageHoursToString() + "|";
        }

        private string EmployeeAverageHoursToString()
        {
            var result = string.Empty;

            if (EmployeeAverageHours != null)
            {
                foreach (var empAvgHours in EmployeeAverageHours)
                {
                    result += empAvgHours;
                }
            }

            var index = result.LastIndexOf(',');

            return index > -1
                   ? $"{result.Remove(index, 1), -70}"
                   : $"{result, -70}";
        }
    }
}
