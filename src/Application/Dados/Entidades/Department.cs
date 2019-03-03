using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dados.Entidades
{
    [Description("")]
    [Table("DEPARTMENTS", Schema = "HR")]
    public class Department
    {
        [Column("DEPARTMENT_NAME")]
        [Description("")]
        public string DepartmentName { get; set; }

        [Column("MANAGER_ID")]
        [Description("")]
        public decimal? ManagerId { get; set; }

        [Column("LOCATION_ID")]
        [Description("")]
        public decimal? LocationId { get; set; }

        [Key]
        [Column("DEPARTMENT_ID")]
        [Description("")]
        public int DepartmentId { get; set; }
    }
}