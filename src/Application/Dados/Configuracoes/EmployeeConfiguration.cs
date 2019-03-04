using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Application.Dados.Entidades;

namespace Application.Dados.Configuracoes
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            Property(p => p.EmployeeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnName("EMPLOYEE_ID")
                .IsRequired();

            Property(p => p.FirstName)
                .HasColumnName("FIRST_NAME")
                .HasMaxLength(20)
                .IsRequired();

            Property(p => p.LastName)
                .HasColumnName("LAST_NAME")
                .HasMaxLength(25)
                .IsRequired();

            Property(p => p.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(25)
                .IsRequired();

            Property(p => p.PhoneNumber)
                .HasColumnName("PHONE_NUMBER")
                .HasMaxLength(20);

            Property(p => p.HireDate)
                .HasColumnName("HIRE_DATE")
                .IsRequired();

            Property(p => p.JobId)
                .HasColumnName("JOB_ID")
                .HasMaxLength(10)
                .IsRequired();

            Property(p => p.Salary)
                .HasColumnName("SALARY");

            Property(p => p.Salary)
                .HasColumnName("COMMISSION_PCT");

            Property(p => p.Salary)
                .HasColumnName("MANAGER_ID");

            Property(p => p.Salary)
                .HasColumnName("DEPARTMENT_ID");
        }
    }
}