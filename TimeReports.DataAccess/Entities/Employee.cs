using System.Collections.Generic;

namespace TimeReports.DataAccess.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TimeReport> TimeReports { get; set; }
    }
}
