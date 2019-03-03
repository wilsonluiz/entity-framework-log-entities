using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dados.Entidades
{
    [Description("")]
    [Table("JOBS", Schema = "HR")]
    public class Job
    {
        [Key]
        [Column("JOB_ID")] [Description("")] public string JobId { get; set; }

        [Column("JOB_TITLE")]
        [Description("")]
        public string JobTitle { get; set; }

        [Column("MIN_SALARY")]
        [Description("")]
        public decimal? MinSalary { get; set; }

        [Column("MAX_SALARY")]
        [Description("")]
        public decimal? MaxSalary { get; set; }
    }
}