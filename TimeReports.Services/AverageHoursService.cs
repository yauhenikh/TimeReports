using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TimeReports.DataAccess.Data;
using TimeReports.Services.Entities;

namespace TimeReports.Services
{
    public class AverageHoursService
    {
        private readonly AppDbContext _dbContext;

        public AverageHoursService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private List<DailyHoursViewModel> GetExistingReports()
        {
            return _dbContext.TimeReports
                .Include(timeReport => timeReport.Employee)
                .ToList()
                .GroupBy(timeReport => timeReport.Date.DayOfWeek)
                .Select(dailyHoursReport => new DailyHoursViewModel
                {
                    Day = dailyHoursReport.Key,
                    EmployeeAverageHours =
                        dailyHoursReport.GroupBy(tr => tr.Employee.Name)
                            .OrderByDescending(employeeHoursReport => employeeHoursReport.Average(tr => tr.Hours))
                            .Take(3)
                            .Select(x => new EmployeeHoursViewModel
                            {
                                Name = x.Key,
                                Hours = (float)Math.Round(x.Average(tr => tr.Hours), 2)
                            })
                            .ToList()
                })
                .ToList();
        }

        private List<DailyHoursViewModel> GetEmptyDayReports()
        {
            var existingDaysOfWeek = GetExistingReports()
                .Select(dailyHoursVM => dailyHoursVM.Day);
            var allDaysOfWeek = Enum.GetValues(typeof(DayOfWeek))
                .OfType<DayOfWeek>();
            var absentDaysOfWeek = allDaysOfWeek
                .Except(existingDaysOfWeek);

            return absentDaysOfWeek.Select(day => new DailyHoursViewModel
            {
                Day = day,
                EmployeeAverageHours = null
            }).ToList();
        }

        private List<DailyHoursViewModel> GetAllDaysReports()
        {
            var existingReports = GetExistingReports();
            existingReports.AddRange(GetEmptyDayReports());

            var allReportsOrderedFromSundayToSaturday = existingReports
                .OrderBy(dailyHoursVM => dailyHoursVM.Day)
                .ToList();
            var allReportsOrderedFromMondayToSunday = allReportsOrderedFromSundayToSaturday
                .Skip(1)
                .ToList();
            allReportsOrderedFromMondayToSunday.Add(allReportsOrderedFromSundayToSaturday.First());

            return allReportsOrderedFromMondayToSunday;
        }

        public string GetAverageHoursOutput()
        {
            var result = string.Empty;

            foreach (var dailyHoursVM in GetAllDaysReports())
            {
                result += $"{dailyHoursVM}\n";
            }

            return result;
        }
    }
}
