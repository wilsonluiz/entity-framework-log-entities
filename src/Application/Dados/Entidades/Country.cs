using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dados.Entidades
{
    [Description("")]
    [Table("COUNTRIES", Schema = "HR")]
    public class Country
    {
        [Column("COUNTRY_NAME")]
        [Description("")]
        public string CountryName { get; set; }

        [Column("REGION_ID")]
        [Description("")]
        public decimal? RegionId { get; set; }

        [Key]
        [Column("COUNTRY_ID")]
        [Description("")]
        public string CountryId { get; set; }
    }
}