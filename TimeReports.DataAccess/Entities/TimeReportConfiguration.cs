using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeReports.DataAccess.Entities
{
    public class TimeReportConfiguration : IEntityTypeConfiguration<TimeReport>
    {
        public void Configure(EntityTypeBuilder<TimeReport> builder)
        {
            builder.HasKey(tr => tr.Id);
            builder.HasOne(tr => tr.Employee).WithMany(e => e.TimeReports).HasForeignKey(tr => tr.EmployeeId);
            builder.Property(tr => tr.Hours).IsRequired();
            builder.Property(tr => tr.Date).IsRequired();
            builder.ToTable("time_reports");
            builder.Property(tr => tr.Id).HasColumnName("id");
            builder.Property(tr => tr.EmployeeId).HasColumnName("employee_id");
            builder.Property(tr => tr.Hours).HasColumnName("hours");
            builder.Property(tr => tr.Date).HasColumnName("date").HasColumnType("date");

            builder.HasData(
                new TimeReport
                {
                    Id = 1,
                    EmployeeId = 42,
                    Hours = 4.5f,
                    Date = new DateTime(2020, 12, 1)
                },
                new TimeReport
                {
                    Id = 2,
                    EmployeeId = 42,
                    Hours = 7.0f,
                    Date = new DateTime(2020, 12, 2)
                },
                new TimeReport
                {
                    Id = 3,
                    EmployeeId = 43,
                    Hours = 5.5f,
                    Date = new DateTime(2020, 12, 1)
                },
                new TimeReport
                {
                    Id = 4,
                    EmployeeId = 43,
                    Hours = 6.0f,
                    Date = new DateTime(2020, 12, 2)
                });
        }
    }
}
