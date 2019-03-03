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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Column("LAST_NAME")]
        public string LastName { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PHONE_NUMBER")]
        public string PhoneNumber { get; set; }

        [Column("HIRE_DATE")]
        public DateTime HireDate { get; set; }

        [Column("JOB_ID")]
        public string JobId { get; set; }

        [Column("SALARY")]
        public decimal? Salary { get; set; }

        [Column("COMMISSION_PCT")]
        public decimal? CommissionPct { get; set; }

        [Column("MANAGER_ID")]
        public decimal? ManagerId { get; set; }

        [Column("DEPARTMENT_ID")]
        public decimal? DepartmentId { get; set; }
    }
}