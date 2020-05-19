using System.Globalization;
using System.Threading;

namespace TimeReports.Services.Entities
{
    public class EmployeeHoursViewModel
    {
        public string Name { get; set; }
        public float Hours { get; set; }

        public override string ToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            return $"{Name} ({Hours} hours), ";
        }
    }
}
