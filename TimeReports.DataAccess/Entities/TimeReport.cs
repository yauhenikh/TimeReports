using System;

namespace TimeReports.DataAccess.Entities
{
    public class TimeReport
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public float Hours { get; set; }
        public DateTime Date { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
