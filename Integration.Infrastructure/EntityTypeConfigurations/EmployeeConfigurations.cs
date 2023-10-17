using Integration.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integration.Infrastructure;

public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(TableNames.Employees);

        builder.HasKey(employee => employee.Id);
        
        builder.Property(employee => employee.PayrollNumber)
            .IsRequired(true);

        builder.Property(employee => employee.Surname)
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(employee => employee.Forenames)
            .IsRequired(false);

        builder.Property(employee => employee.DateOfBirth)
            .IsRequired(false);

        builder.Property(employee => employee.Telephone)
            .IsRequired(false);

        builder.Property(employee => employee.Mobile)
            .IsRequired(false);

        builder.Property(employee => employee.Address)
            .IsRequired(false);

        builder.Property(employee => employee.Address2)
            .IsRequired(false);

        builder.Property(employee => employee.Postcode)
            .IsRequired(false);

        builder.Property(employee => employee.Email)
            .IsRequired(false);

        builder.Property(employee => employee.StartDate)
            .IsRequired(false);
    }
}
