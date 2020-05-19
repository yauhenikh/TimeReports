using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeReports.DataAccess.Entities
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.ToTable("employees");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name).HasColumnName("name");

            builder.HasData(
                new Employee
                {
                    Id = 42,
                    Name = "John"
                },
                new Employee
                {
                    Id = 43,
                    Name = "Jane"
                });
        }
    }
}
