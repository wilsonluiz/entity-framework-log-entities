using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dados.Entidades
{
    [Description("")]
    [Table("REGIONS", Schema = "HR")]
    public class Region
    {
        [Key]
        [Column("REGION_ID")]
        [Description("")]
        public decimal RegionId { get; set; }

        [Column("REGION_NAME")]
        [Description("")]
        public string RegionName { get; set; }
    }
}