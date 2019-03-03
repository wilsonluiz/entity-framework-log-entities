using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dados.Entidades
{
    [Description("")]
    [Table("EMPLOYEES", Schema = "HR")]
    public class Employee
    {
        [Key]
        [Column("EMPLOYEE_ID")]
        [Description("")]
        public int EmployeeId { get; private set; }

        [Column("FIRST_NAME")]
        [Description("")]
        public string FirstName { get; private set; }

        [Column("LAST_NAME")]
        [Description("")]
        public string LastName { get; private set; }

        [Column("EMAIL")] [Description("")] public string Email { get; private set; }

        [Column("PHONE_NUMBER")]
        [Description("")]
        public string PhoneNumber { get; private set; }

        [Column("HIRE_DATE")]
        [Description("")]
        public DateTime HireDate { get; private set; }

        [Column("JOB_ID")] [Description("")] public string JobId { get; private set; }

        [Column("SALARY")] [Description("")] public decimal? Salary { get; private set; }

        [Column("COMMISSION_PCT")]
        [Description("")]
        public decimal? CommissionPct { get; private set; }

        [Column("MANAGER_ID")]
        [Description("")]
        public decimal? ManagerId { get; private set; }

        [Column("DEPARTMENT_ID")]
        [Description("")]
        public decimal? DepartmentId { get; private set; }
    }
}